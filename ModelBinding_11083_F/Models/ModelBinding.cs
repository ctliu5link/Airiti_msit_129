using ModelBinding_11083.Models;
using ModelBinding_11083.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelBinding_11083.Models
{
    public static class ModelBinding
    {
        public static List<DataBindingViewModel> getList()
        {
            Person Tom = new Person() { Name = "Tom", Age = 15 };
            Person Vivian = new Person() { Name = "Vivian", Age = 20 };
            Person Mike = new Person() { Name = "Mike", Age = 17, Friends = new List<Person> { Tom, Vivian } };


            List<DataBindingViewModel> list = new List<DataBindingViewModel>();
            list.Add(new DataBindingViewModel(
                    "SimpleBinding", "text", "(string Name, int Age)",
                    new { Name = "Tom", Age = 15 }));

            list.Add(new DataBindingViewModel(
                "ModelBindObj", "json", "(Human data)",
                (Human)Tom));

            list.Add(new DataBindingViewModel(
                "SimpleModelBindArray", "text", "(string[] Name, int[] Age)",
                new { Name = new string[] { "Tom", "Mike" }, Age = new int[] { 15, 16 } }));

            list.Add(new DataBindingViewModel(
                 "ModelBindingArray", "json", "(Human[] data)",
                 new { data = new Human[] { Tom, Mike } }));

            list.Add(new DataBindingViewModel(
                  "ModelBindingNestedObj", "json", "(Person data)",
                  Tom));

            list.Add(new DataBindingViewModel(
                  "ModelBindingArrayNestedObj", "json", "(Person[] data)",
                   new { data = new Person[] { Tom, Mike, Vivian } }));

            return list;
        }

    }
}