﻿using Application.Common.Abstractions.DataContexts;

namespace ClientMauiApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        IApplicationDbContext _dbContext;
        public MainPage(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
