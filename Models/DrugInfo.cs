using System.Collections.Generic;
namespace WebAppDemoStudent.Models
{
    public class DrugInfo
    {
        public string MedicineName { get; set; }
        public string GeneralName { get; set; }
        public string Route { get; set; }
        public string PharmClass { get; set; }
        public string DrugIndication { get; set; }
        public List<ReactionModel> Reactions { get; set; }
    }

    public class ReactionModel
    {
        public string ReactionMedDrapt { get; set; }
    }
}
