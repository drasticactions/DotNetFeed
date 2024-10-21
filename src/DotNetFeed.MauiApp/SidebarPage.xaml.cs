// <copyright file="SidebarPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotNetFeed.ViewModels;

namespace DotNetFeed;

public partial class SidebarPage : ContentPage
{
	private SidebarViewModel viewModel;

    public SidebarPage(SidebarViewModel viewModel)
    {
        this.InitializeComponent();
		this.viewModel = viewModel;
        this.BindingContext = this.ViewModel;
    }

	public SidebarViewModel ViewModel => this.viewModel;
}