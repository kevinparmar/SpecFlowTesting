Feature: AddToCart

As a user
I want to add items to my shopping cart
So that I can review and purchase them

@loginRequired
Scenario: Add to Cart as a Logged-in User
    Given I am a logged-in user
    And I am on a product page
    When I click the Add to Cart button
    And I click on the cart icon
    Then I should see the item in the cart

@loginRequired
Scenario: Add to Cart as a Not Logged-in User
  Given I am a not logged-in user
  And I am on a product page
  When I click the Add to Cart button
  Then I should be redirected to the login page
