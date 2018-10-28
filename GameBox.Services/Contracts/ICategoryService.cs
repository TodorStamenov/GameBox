﻿using GameBox.Core;
using GameBox.Services.Models.Binding.Categories;
using GameBox.Services.Models.View.Categories;
using System;
using System.Collections.Generic;

namespace GameBox.Services.Contracts
{
    public interface ICategoryService
    {
        ServiceResult Create(string name);

        ServiceResult Edit(Guid id, string name);

        CategoryBindingModel Get(Guid id);

        IEnumerable<ListMenuCategoriesViewModel> Menu();

        IEnumerable<ListCategoriesViewModel> All();
    }
}