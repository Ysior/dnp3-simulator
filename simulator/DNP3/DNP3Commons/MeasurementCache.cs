using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using System.Threading;

using Automatak.DNP3.Interface;

namespace Automatak.Simulator.DNP3.Commons
{
       
    public class MeasurementCache: ISOEHandler, IMeasurementCache, IDatabase, IMeasurementLoader
    {
        readonly Object mutex = new Object();

        readonly MeasurementCollection binaries = new MeasurementCollection();
        readonly MeasurementCollection doubleBinaries = new MeasurementCollection();
        readonly MeasurementCollection counters = new MeasurementCollection();
        readonly MeasurementCollection frozenCounters = new MeasurementCollection();
        readonly MeasurementCollection analogs = new MeasurementCollection();
        readonly MeasurementCollection binaryOutputStatii = new MeasurementCollection();
        readonly MeasurementCollection analogOutputStatii = new MeasurementCollection();
        readonly MeasurementCollection octetStrings = new MeasurementCollection();
        readonly MeasurementCollection timeAndIntervals = new MeasurementCollection();
        
        public MeasurementCache(DatabaseTemplate template)
        {                        
            var values = new ChangeSet();
            var restart = new Flags(2);
            template.binary.Keys.ToList().ForEach(index => values.Update(new Binary(restart), index));
            template.doubleBinary.Keys.ToList().ForEach(index => values.Update(new DoubleBitBinary(restart), index));
            template.counter.Keys.ToList().ForEach(index => values.Update(new Counter(restart), index));
            template.analog.Keys.ToList().ForEach(index => values.Update(new Analog(restart), index));
            template.binaryOutputStatus.Keys.ToList().ForEach(index => values.Update(new BinaryOutputStatus(restart), index));
            template.analogOutputStatus.Keys.ToList().ForEach(index => values.Update(new AnalogOutputStatus(restart), index));
            template.timeAndInterval.Keys.ToList().ForEach(index => values.Update(new TimeAndInterval(0, 0, IntervalUnits.Undefined), index));

            this.Load(values);
        }

        public void Load(IChangeSet updates)
        {
            updates.Apply(this);
        }

        public MeasurementCache()
        {

        }

        void ISOEHandler.BeginFragment(ResponseInfo info)
        {
            Monitor.Enter(mutex);
        }      
        
        void ISOEHandler.EndFragment(ResponseInfo info)
        {
            Monitor.Exit(mutex);          
        }       

        MeasurementCollection GetCollectionMaybeNull(MeasType type)
        {
            switch (type)
            {
                case (MeasType.Binary):
                    return binaries;
                case (MeasType.DoubleBitBinary):
                    return doubleBinaries;
                case (MeasType.Counter):
                    return counters;
                case (MeasType.FrozenCounter):
                    return frozenCounters;
                case (MeasType.Analog):
                    return analogs;
                case (MeasType.BinaryOutputStatus):
                    return binaryOutputStatii;
                case (MeasType.AnalogOutputStatus):
                    return analogOutputStatii;
                case (MeasType.OctetString):
                    return octetStrings;
                case(MeasType.TimeAndInterval):
                    return timeAndIntervals;
                default:
                    return null;
            }
        }

        MeasurementCollection IMeasurementCache.Binaries
        {
            get { return binaries; }
        }

        MeasurementCollection IMeasurementCache.DoubleBinaries
        {
            get { return doubleBinaries; }
        }

        MeasurementCollection IMeasurementCache.Counters
        {
            get { return counters; }
        }

        MeasurementCollection IMeasurementCache.FrozenCounters
        {
            get { return frozenCounters; }
        }

        MeasurementCollection IMeasurementCache.Analogs
        {
            get { return analogs; }
        }

        MeasurementCollection IMeasurementCache.BinaryOutputStatii
        {
            get { return binaryOutputStatii; }
        }

        MeasurementCollection IMeasurementCache.AnalogOutputStatii
        {
            get { return analogOutputStatii; }
        }

        public MeasurementCollection OctetStrings
        {
            get { return octetStrings; }
        }

        public MeasurementCollection GetCollection(MeasType type)
        {
            switch (type)
            { 
                case(MeasType.Binary):
                    return binaries;
                case (MeasType.DoubleBitBinary):
                    return doubleBinaries;
                case (MeasType.Counter):
                    return counters;
                case (MeasType.FrozenCounter):
                    return frozenCounters;
                case (MeasType.Analog):
                    return analogs;
                case (MeasType.BinaryOutputStatus):
                    return binaryOutputStatii;
                case (MeasType.AnalogOutputStatus):
                    return analogOutputStatii;
                case(MeasType.OctetString):
                    return octetStrings;
                default:
                    return null;
            }
        }
        

        void IDatabase.Update(Binary update, ushort index, EventMode mode)
        {
            binaries.Update(update.ToMeasurement(index, TimestampMode.SYNCHRONIZED));            
        }

        void IDatabase.Update(DoubleBitBinary update, ushort index, EventMode mode)
        {
            doubleBinaries.Update(update.ToMeasurement(index, TimestampMode.SYNCHRONIZED));            
        }

        void IDatabase.Update(Analog update, ushort index, EventMode mode)
        {
            analogs.Update(update.ToMeasurement(index, TimestampMode.SYNCHRONIZED));            
        }

        void IDatabase.Update(Counter update, ushort index, EventMode mode)
        {
            counters.Update(update.ToMeasurement(index, TimestampMode.SYNCHRONIZED));            
        }

        void IDatabase.Update(BinaryOutputStatus update, ushort index, EventMode mode)
        {
            binaryOutputStatii.Update(update.ToMeasurement(index, TimestampMode.SYNCHRONIZED));            
        }

        void IDatabase.Update(AnalogOutputStatus update, ushort index, EventMode mode)
        {
            analogOutputStatii.Update(update.ToMeasurement(index, TimestampMode.SYNCHRONIZED));            
        }

        void IDatabase.FreezeCounter(ushort index, bool clear, EventMode mode) { }

        void IDatabase.Update(OctetString update, ushort index, EventMode mode)
        {
            octetStrings.Update(update.ToMeasurement(index, TimestampMode.SYNCHRONIZED));
        }

        void IDatabase.Update(TimeAndInterval update, ushort index)
        {
            timeAndIntervals.Update(update.ToMeasurement(index, TimestampMode.SYNCHRONIZED));            
        }


        void ISOEHandler.Process(HeaderInfo info, IEnumerable<IndexedValue<Binary>> values)
        {
            var converted = values.Select(m => m.Value.ToMeasurement(m.Index, info.tsquality.ToTimestampMode()));
            binaries.Update(converted);
        }

        void ISOEHandler.Process(HeaderInfo info, IEnumerable<IndexedValue<DoubleBitBinary>> values)
        {
            var converted = values.Select(m => m.Value.ToMeasurement(m.Index, info.tsquality.ToTimestampMode()));
            doubleBinaries.Update(converted);
        }

        void ISOEHandler.Process(HeaderInfo info, IEnumerable<IndexedValue<Analog>> values)
        {
            var converted = values.Select(m => m.Value.ToMeasurement(m.Index, info.tsquality.ToTimestampMode()));
            analogs.Update(converted);
        }

        void ISOEHandler.Process(HeaderInfo info, IEnumerable<IndexedValue<Counter>> values)
        {
            var converted = values.Select(m => m.Value.ToMeasurement(m.Index, info.tsquality.ToTimestampMode()));
            counters.Update(converted);
        }

        void ISOEHandler.Process(HeaderInfo info, IEnumerable<IndexedValue<FrozenCounter>> values)
        {
            var converted = values.Select(m => m.Value.ToMeasurement(m.Index, info.tsquality.ToTimestampMode()));
            frozenCounters.Update(converted);
        }

        void ISOEHandler.Process(HeaderInfo info, IEnumerable<IndexedValue<BinaryOutputStatus>> values)
        {
            var converted = values.Select(m => m.Value.ToMeasurement(m.Index, info.tsquality.ToTimestampMode()));
            binaryOutputStatii.Update(converted);
        }

        void ISOEHandler.Process(HeaderInfo info, IEnumerable<IndexedValue<AnalogOutputStatus>> values)
        {
            var converted = values.Select(m => m.Value.ToMeasurement(m.Index, info.tsquality.ToTimestampMode()));
            analogOutputStatii.Update(converted);
        }

        void ISOEHandler.Process(HeaderInfo info, IEnumerable<IndexedValue<OctetString>> values)
        {
            var converted = values.Select(m => m.Value.ToMeasurement(m.Index, info.tsquality.ToTimestampMode()));
            timeAndIntervals.Update(converted);
        }

        void ISOEHandler.Process(HeaderInfo info, IEnumerable<IndexedValue<TimeAndInterval>> values)
        {
            var converted = values.Select(m => m.Value.ToMeasurement(m.Index, info.tsquality.ToTimestampMode()));
            octetStrings.Update(converted);
        }

        void ISOEHandler.Process(HeaderInfo info, IEnumerable<IndexedValue<BinaryCommandEvent>> values)
        {
            // TODO
        }

        void ISOEHandler.Process(HeaderInfo info, IEnumerable<IndexedValue<AnalogCommandEvent>> values)
        {
            // TODO
        }

        void ISOEHandler.Process(HeaderInfo info, IEnumerable<IndexedValue<SecurityStat>> values)
        {
            // TODO
        }
    }
}
