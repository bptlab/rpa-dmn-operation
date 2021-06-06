using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.ComponentModel.Design;
using bptlab.DMNActivities.Activities.Design.Designers;
using bptlab.DMNActivities.Activities.Design.Properties;

namespace bptlab.DMNActivities.Activities.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();
            builder.ValidateTable();

            var categoryAttribute = new CategoryAttribute($"{Resources.Category}");

            builder.AddCustomAttributes(typeof(ExternalDecisionService), categoryAttribute);
            builder.AddCustomAttributes(typeof(ExternalDecisionService), new DesignerAttribute(typeof(ExternalDecisionServiceDesigner)));
            builder.AddCustomAttributes(typeof(ExternalDecisionService), new HelpKeywordAttribute(""));


            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
