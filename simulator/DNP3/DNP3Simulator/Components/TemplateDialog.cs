using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Automatak.DNP3.Interface;

namespace Automatak.Simulator.DNP3.Components
{
    partial class TemplateDialog : Form
    {                
        public TemplateDialog(string alias, DatabaseTemplate template)
        {
            InitializeComponent();

            this.textBoxAlias.Text = alias;

            Configure(template);
        }

        private void buttonADD_Click(object sender, EventArgs e)
        {            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public String SelectedAlias
        {
            get
            {
                return textBoxAlias.Text;
            }
        }

        public DatabaseTemplate ConfiguredTemplate
        {
            get
            {
                var template = new DatabaseTemplate();
                                
                template.doubleBinary = templateControlDoubleBinary.GetRecords().ToDictionary(rec => rec.index, rec => new DoubleBinaryConfig { clazz = rec.clazz });
                template.counter = templateControlCounter.GetRecords().ToDictionary(rec => rec.index, rec => new CounterConfig { clazz = rec.clazz });
                template.frozenCounter = templateControlFrozenCounter.GetRecords().ToDictionary(rec => rec.index, rec => new FrozenCounterConfig { clazz = rec.clazz });
                template.analog = templateControlAnalog.GetRecords().ToDictionary(rec => rec.index, rec => new AnalogConfig { clazz = rec.clazz });
                template.binaryOutputStatus = templateControlBOStatus.GetRecords().ToDictionary(rec => rec.index, rec => new BinaryOutputStatusConfig { clazz = rec.clazz });
                template.analogOutputStatus = templateControlAOStatus.GetRecords().ToDictionary(rec => rec.index, rec => new AnalogOutputStatusConfig { clazz = rec.clazz });


                return template;
            }
        }

        private void Configure(DatabaseTemplate template)
        {
            this.templateControlAnalog.SetRecords(template.analog.Select(rec => new EventRecord(rec.Key, rec.Value.clazz)));
            this.templateControlAOStatus.SetRecords(template.analogOutputStatus.Select(rec => new EventRecord(rec.Key, rec.Value.clazz)));
            this.templateControlBinary.SetRecords(template.binary.Select(rec => new EventRecord(rec.Key, rec.Value.clazz)));
            this.templateControlBOStatus.SetRecords(template.binaryOutputStatus.Select(rec => new EventRecord(rec.Key, rec.Value.clazz)));
            this.templateControlCounter.SetRecords(template.counter.Select(rec => new EventRecord(rec.Key, rec.Value.clazz)));
            this.templateControlDoubleBinary.SetRecords(template.doubleBinary.Select(rec => new EventRecord(rec.Key, rec.Value.clazz)));
            this.templateControlFrozenCounter.SetRecords(template.frozenCounter.Select(rec => new EventRecord(rec.Key, rec.Value.clazz)));
        }
      
    }
}
