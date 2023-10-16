using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowProjectPizzaShop.StepDefinitions
{
    [Binding]
    public class SearchStepDefinitions
    {
        private IWebDriver _driver;
        private string existingPizza = "Chicken";
        private string nonExistingPizza = "Paneer";

        public SearchStepDefinitions(IWebDriver driver) 
        {
            _driver = driver;
        }

        [Given(@"I am on the pizza search page")]
        public void GivenIAmOnThePizzaSearchPage()
        {
            _driver.Navigate().GoToUrl("https://localhost:7013/");
        }

        [When(@"I enter an existing pizza into the search input")]
        public void WhenIEnterAnExistingPizzaIntoTheSearchInput()
        {
            IWebElement searchBox = _driver.FindElement(By.XPath("//*[@id=\"searchTerm\"]"));
            searchBox.Clear();
            searchBox.SendKeys(existingPizza);
        }

        [When(@"I click the search button")]
        public void WhenIClickTheSearchButton()
        {
            IWebElement searchButton = _driver.FindElement(By.XPath("//*[@id=\"Search_Button\"]"));
            searchButton.Click();
        }

        [Then(@"I should see the pizza in the results")]
        public void ThenIShouldSeeThePizzaInTheResults()
        {
            string searchQuery = existingPizza; // Replace with your search term
            bool pizzaFound = false;

            IWebElement pizzaContainer = _driver.FindElement(By.XPath("//*[@id=\"Pizza_Container\"]"));
            IList<IWebElement> pizzaCards = pizzaContainer.FindElements(By.CssSelector(".card#Pizza_Card"));

            foreach (var card in pizzaCards)
            {
                IWebElement PizzaNameElement = _driver.FindElement(By.XPath("//*[@id=\"Pizza_Name\"]"));
                IWebElement PizzaDescElement = _driver.FindElement(By.XPath("//*[@id=\"Pizza_Description\"]"));
                
                string pizzaName = PizzaNameElement.Text;
                string pizzaDescription = PizzaDescElement.Text;

                if (pizzaName.Contains(searchQuery) || pizzaDescription.Contains(searchQuery))
                {
                    pizzaFound = true;
                    break;
                }
            }

            Assert.IsTrue(pizzaFound, $"Expected pizza with '{searchQuery}' in the name or description not found in the results.");
        }   

        [When(@"I enter non existing pizza into the search input")]
        public void WhenIEnterNonExistingPizzaIntoTheSearchInput()
        {
            IWebElement searchBox = _driver.FindElement(By.XPath("//*[@id=\"searchTerm\"]"));
            searchBox.Clear();
            searchBox.SendKeys(nonExistingPizza);
        }

        [Then(@"I should see some error message")]
        public void ThenIShouldSeeSomeErrorMessage()
        {
            IWebElement errorMessage = _driver.FindElement(By.XPath("//*[@id=\"No_Pizza_Found\"]"));
            Assert.IsTrue(errorMessage.Text.Contains("No pizza found"));
        }

        [When(@"I enter a pizza into the search input")]
        public void WhenIEnterAPizzaIntoTheSearchInput()
        {
            IWebElement searchBox = _driver.FindElement(By.XPath("//*[@id=\"searchTerm\"]"));
            searchBox.Clear();
            searchBox.SendKeys(nonExistingPizza);
        }

        [When(@"I see search results")]
        public void WhenISeeSearchResults()
        {
            IWebElement pizzaContainer = _driver.FindElement(By.XPath("//*[@id=\"Pizza_Container\"]"));

            IList<IWebElement> pizzaCards = pizzaContainer.FindElements(By.CssSelector("div.card.Pizza_Card"));

            IWebElement noPizzaFoundMessage = _driver.FindElement(By.XPath("//*[@id=\"No_Pizza_Found\"]"));

            Assert.IsTrue(pizzaCards.Count > 0 || noPizzaFoundMessage.Displayed);
        }

        [When(@"I click the reset button")]
        public void WhenIClickTheResetButton()
        {
            IWebElement searchButton = _driver.FindElement(By.XPath("//*[@id=\"Reset_Link\"]"));
            searchButton.Click();
        }

        [Then(@"I should see all available pizzas")]
        public void ThenIShouldSeeAllAvailablePizzas()
        {
            IWebElement pizzaContainer = _driver.FindElement(By.XPath("//*[@id=\"Pizza_Container\"]"));
            IList<IWebElement> pizzaCards = pizzaContainer.FindElements(By.CssSelector(".card#Pizza_Card"));
            Assert.IsTrue(pizzaCards.Count > 0, "No pizzas found. Expected to see all available pizzas.");
        }
    }
}
