using System.Collections.Generic;
using WebStore.DomainCore.Entities;
using WebStore.Models;

namespace WebStore.Data
{
    public class TestData
    {
        public static List<Employee> Employees { get; } = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                FirstName = "Ivan",
                SurName = "Ivanov",
                Patronymic = "Ivanovich",
                Age = 54
            },
            new Employee
            {
                Id = 2,
                FirstName = "Sergey",
                SurName = "Petrov",
                Patronymic = "Bogdanovich",
                Age = 28
            },
            new Employee
            {
                Id = 3,
                FirstName = "Boris",
                SurName = "Grozniy",
                Patronymic = "Ivanovich",
                Age = 19
            }
        };

        public static IEnumerable<Brand> Brands { get; } = new[]
        {
            new Brand(){Id = 1, Name = "Acne", Order = 0},
            new Brand(){Id = 2, Name = "Grune Erde", Order = 1},
            new Brand(){Id = 3, Name = "Albiro", Order = 2},
            new Brand(){Id = 4, Name = "Ronhill", Order = 3},
        };

        public static IEnumerable<Section> Sections { get; } = new[]
        {
            new Section {Id = 1, Name = "Спорт", Order = 0},
            new Section {Id = 2, Name = "Nike", Order = 0 , ParentSectionId = 1},
            new Section {Id = 3, Name = "Under Armour",  Order = 1, ParentSectionId = 1},
            new Section {Id = 4, Name = "Adidas",  Order = 2, ParentSectionId = 1},

            new Section {Id = 7, Name = "Для мужчин",  Order = 1},
            new Section {Id = 8, Name = "Fendi",  Order = 0, ParentSectionId = 7},
            new Section {Id = 9, Name = "Boss",  Order = 1, ParentSectionId = 7},

            new Section {Id = 18, Name = "Для женщин",  Order = 2},
            new Section {Id = 19, Name = "Guchi",  Order = 0, ParentSectionId = 18},
            new Section {Id = 20, Name = "Guess",  Order = 1, ParentSectionId = 18},


            new Section {Id = 21, Name = "Аксессуары",  Order = 3},
        };

        public static IEnumerable<Product> Products { get; } = new[]
        {
            new Product { Id = 1, Name = "Белое платье", Price = 1025, ImageUrl = "product1.jpg", Order = 0, SectionId = 2, BrandId = 1 },
            new Product { Id = 2, Name = "Розовое платье", Price = 1025, ImageUrl = "product2.jpg", Order = 1, SectionId = 2, BrandId = 2 },
            new Product { Id = 3, Name = "Красное платье", Price = 1025, ImageUrl = "product3.jpg", Order = 2, SectionId = 2, BrandId = 3 },
            new Product { Id = 4, Name = "Джинсы", Price = 1025, ImageUrl = "product4.jpg", Order = 3, SectionId = 3, BrandId = 4 },
            new Product { Id = 5, Name = "Лёгкая майка", Price = 1025, ImageUrl = "product5.jpg", Order = 4, SectionId = 3, BrandId = 2 },
            new Product { Id = 6, Name = "Лёгкое голубое поло", Price = 1025, ImageUrl = "product6.jpg", Order = 5, SectionId = 4, BrandId = 1 },
            new Product { Id = 7, Name = "Платье белое", Price = 1025, ImageUrl = "product7.jpg", Order = 6, SectionId = 8, BrandId = 1 },
            new Product { Id = 8, Name = "Костюм кролика", Price = 1025, ImageUrl = "product8.jpg", Order = 7, SectionId = 9, BrandId = 1 },
            new Product { Id = 9, Name = "Красное китайское платье", Price = 1025, ImageUrl = "product9.jpg", Order = 8, SectionId = 18, BrandId = 4 },
            new Product { Id = 10, Name = "Женские джинсы", Price = 1025, ImageUrl = "product10.jpg", Order = 9, SectionId = 19, BrandId = 3 },
            new Product { Id = 11, Name = "Джинсы женские", Price = 1025, ImageUrl = "product11.jpg", Order = 10, SectionId = 19, BrandId = 3 },
            new Product { Id = 12, Name = "Летний костюм", Price = 1025, ImageUrl = "product12.jpg", Order = 11, SectionId = 20, BrandId = 3 },
        };
    }
}