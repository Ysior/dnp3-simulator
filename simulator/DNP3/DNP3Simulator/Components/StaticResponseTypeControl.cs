using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Automatak.DNP3.Interface;

namespace Automatak.Simulator.DNP3.Components
{
    public partial class StaticResponseTypeControl : ResponseTypeControl
    {
        public StaticResponseTypeControl()
        {
            InitializeComponent();

            this.ComboBoxBinary.DataSource = Enum.GetValues(typeof(StaticBinaryVariation));
            this.ComboBoxDoubleBinary.DataSource = Enum.GetValues(typeof(StaticDoubleBinaryVariation));
            this.ComboBoxCounter.DataSource = Enum.GetValues(typeof(StaticCounterVariation));
            this.ComboBoxFrozenCounter.DataSource = Enum.GetValues(typeof(StaticFrozenCounterVariation));
            this.ComboBoxAnalog.DataSource = Enum.GetValues(typeof(StaticAnalogVariation));
            this.ComboBoxBinaryOutputStatus.DataSource = Enum.GetValues(typeof(StaticBinaryOutputStatusVariation));
            this.ComboBoxAnalogOutputStatus.DataSource = Enum.GetValues(typeof(StaticAnalogOutputStatusVariation));
        }

        // TODO remove this
        public void Configure(DatabaseTemplate template)
        {
            if (ComboBoxBinary.SelectedItem is StaticBinaryVariation binary) foreach (var record in template.binary.Values) record.staticVariation = binary;
            if (ComboBoxBinaryOutputStatus.SelectedItem is StaticBinaryOutputStatusVariation binaryOutputStatus) foreach (var record in template.binaryOutputStatus.Values) record.staticVariation = binaryOutputStatus;
            if (ComboBoxDoubleBinary.SelectedItem is StaticDoubleBinaryVariation doubleBinary) foreach (var record in template.doubleBinary.Values) record.staticVariation = doubleBinary;
            if (ComboBoxCounter.SelectedItem is StaticCounterVariation counter) foreach (var record in template.counter.Values) record.staticVariation = counter;
            if (ComboBoxCounter.SelectedItem is StaticFrozenCounterVariation frozenCounter) foreach (var record in template.frozenCounter.Values) record.staticVariation = frozenCounter;
            if (ComboBoxAnalog.SelectedItem is StaticAnalogVariation analog) foreach (var record in template.analog.Values) record.staticVariation = analog;
            if (ComboBoxAnalogOutputStatus.SelectedItem is StaticAnalogOutputStatusVariation analogOutputStatus) foreach (var record in template.analogOutputStatus.Values) record.staticVariation = analogOutputStatus;
        }
    }
}
