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
    public partial class EventResponseTypeControl : ResponseTypeControl
    {
        public EventResponseTypeControl()
        {
            InitializeComponent();

            this.ComboBoxBinary.DataSource = Enum.GetValues(typeof(EventBinaryVariation));
            this.ComboBoxDoubleBinary.DataSource = Enum.GetValues(typeof(EventDoubleBinaryVariation));
            this.ComboBoxCounter.DataSource = Enum.GetValues(typeof(EventCounterVariation));
            this.ComboBoxFrozenCounter.DataSource = Enum.GetValues(typeof(EventFrozenCounterVariation));
            this.ComboBoxAnalog.DataSource = Enum.GetValues(typeof(EventAnalogVariation));
            this.ComboBoxBinaryOutputStatus.DataSource = Enum.GetValues(typeof(EventBinaryOutputStatusVariation));
            this.ComboBoxAnalogOutputStatus.DataSource = Enum.GetValues(typeof(EventAnalogOutputStatusVariation));
        }

        // TODO remove this
        public void Configure(DatabaseTemplate template)
        {
            if (ComboBoxBinary.SelectedItem is EventBinaryVariation binary) foreach (var record in template.binary.Values) record.eventVariation = binary;
            if (ComboBoxBinaryOutputStatus.SelectedItem is EventBinaryOutputStatusVariation binaryOutputStatus) foreach (var record in template.binaryOutputStatus.Values) record.eventVariation = binaryOutputStatus;
            if (ComboBoxDoubleBinary.SelectedItem is EventDoubleBinaryVariation doubleBinary) foreach (var record in template.doubleBinary.Values) record.eventVariation = doubleBinary;
            if (ComboBoxCounter.SelectedItem is EventCounterVariation counter) foreach (var record in template.counter.Values) record.eventVariation = counter;
            if (ComboBoxCounter.SelectedItem is EventFrozenCounterVariation frozenCounter) foreach (var record in template.frozenCounter.Values) record.eventVariation = frozenCounter;
            if (ComboBoxAnalog.SelectedItem is EventAnalogVariation analog) foreach (var record in template.analog.Values) record.eventVariation = analog;
            if (ComboBoxAnalogOutputStatus.SelectedItem is EventAnalogOutputStatusVariation analogOutputStatus) foreach (var record in template.analogOutputStatus.Values) record.eventVariation = analogOutputStatus;
        }
    }
}
