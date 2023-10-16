Feature: Search
  As a user
  I want to search for pizzas
  So that I can find the one I want to order

  Scenario: Search for a Pizza and Find It
    Given I am on the pizza search page
    When I enter an existing pizza into the search input
    And I click the search button
    Then I should see the pizza in the results

  Scenario: Search for a Pizza and Not Find It
    Given I am on the pizza search page
    When I enter non existing pizza into the search input
    And I click the search button
    Then I should see some error message 

  Scenario: Reset the Search
    Given I am on the pizza search page
    When I enter a pizza into the search input
    And I click the search button
    And I see search results
    When I click the reset button
    Then I should see all available pizzas


