using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace UniHub.API.Conventions;

public class DefaultProducesConvention : IApplicationModelConvention
{
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            controller.Filters.Add(new ProducesAttribute("application/json"));
        }
    }
}
