Feature: CartDetails

As a user
I want to view and manage items in my shopping cart
So that I can complete my purchase

@loginRequired
@cartItemsNeeded
Scenario: Number of unique Cart items and cart badge number should match
    Given I am on the Cart Details page
    Then the number of unique Cart items should match the cart badge number

@loginRequired
@cartItemsNeeded
Scenario: When I Increase Quantity, it should reflect in the table
    Given I am on the Cart Details page
    When I increase the quantity of an item in the cart
    Then the table should reflect the updated quantity

@loginRequired
@cartItemsNeeded
Scenario: When I Decrease Quantity, it should reflect in the table
    Given I am on the Cart Details page
    When I decrease the quantity of an item in the cart
    Then the table should reflect the updated quantity

@loginRequired
Scenario: Clicking on Shop More button should take me back to homepage
    Given I am on the Cart Details page
    When I click on the Shop More button
    Then I should be redirected to the homepage

@loginRequired
@cartItemsNeeded
Scenario: Checkout button should take me to the checkout page
    Given I am on the Cart Details page
    When I click on the Checkout button
    Then I should be redirected to the checkout page

@loginRequired
@cartItemsNeeded
Scenario: Empty Cart button should empty cart
    Given I am on the Cart Details page
    When I click on the Empty Cart button
    Then Cart empty message should be displayed

