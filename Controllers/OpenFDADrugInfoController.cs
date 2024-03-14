using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppDemoStudent.Models;

namespace WebAppDemoStudent.Controllers
{
    public class OpenFDADrugInfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Search(string searchTerm)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync($"https://api.fda.gov/drug/event.json?search={searchTerm}&limit=4");
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(responseData);
                        var results = data.ContainsKey("results") ? data["results"] : null;

                        if (results != null && results.Count > 0)
                        {
                            var drugInfoList = new List<DrugInfo>();
                            foreach (var result in results)
                            {
                                var patient = result["patient"];
                                var drug = patient["drug"][0];

                                var drugInfo = new DrugInfo
                                {
                                    MedicineName = drug["medicinalproduct"],
                                    GeneralName = drug["openfda"]?["generic_name"]?.First ?? "N/A",
                                    Route = drug["openfda"]?["route"]?.First ?? "N/A",
                                    PharmClass = drug["openfda"]?["pharm_class_cs"]?.First ?? "N/A",
                                    DrugIndication = drug["drugindication"] ?? "N/A",
                                    Reactions = new List<ReactionModel>()
                                };

                                foreach (var reaction in patient["reaction"])
                                {
                                    drugInfo.Reactions.Add(new ReactionModel
                                    {
                                        ReactionMedDrapt = reaction["reactionmeddrapt"]
                                    });
                                }

                                drugInfoList.Add(drugInfo);
                            }

                            return View(drugInfoList);
                        }
                        else
                        {
                            ViewBag.Error = "No drug information found.";
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Error fetching drug information. Please try again.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error fetching data: {ex.Message}";
            }

            return View(new List<DrugInfo>());
        }
    }
}
