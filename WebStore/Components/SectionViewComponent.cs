using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainCore.Entities;
using WebStore.Infastrature.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    public class SectionViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public SectionViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public IViewComponentResult Invoke()
        {
            return View(GetSections());
        }

        private List<SectionViewModel> GetSections()
        {
            var allSection = _productData.GetSections().ToList();

            var mainSection = allSection.Where(_ => _.ParentSectionId is null).ToList();

            var mainSectionVM = new List<SectionViewModel>();

            foreach (var section in mainSection)
            {
                mainSectionVM.Add(new SectionViewModel()
                {
                    Id = section.Id,
                    Name = section.Name,
                    Order = section.Order,
                    ParentSection = null
                });
            }

            foreach (var section in mainSectionVM)
            {
                var childs = allSection.Where(_ => _.ParentSectionId == section.Id);
                foreach (var child in childs)
                {
                    section.ChildSections.Add(new SectionViewModel()
                    {
                        Order = child.Order,
                        ParentSection = section,
                        Id = child.Id,
                        Name = child.Name
                    });
                }

                section.ChildSections = section.ChildSections.OrderBy(_ => _.Order).ToList();
            }

            mainSectionVM = mainSectionVM.OrderBy(_ => _.Order).ToList();

            return mainSectionVM;
        }
    }
}
