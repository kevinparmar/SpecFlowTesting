Feature: ConfirmOrder

As a user
I want to confirm my order
So that I can proceed with the purchase

@loginRequired 
@cartItemsNeeded
Scenario: Check if Order details are displayed
  Given I am on the Cart Details Page
  When I press the Checkout Button
  Then I should be on the Order Details Page
  And I should see the Order Details table

@loginRequired 
@cartItemsNeeded
Scenario: Check if Confirm Button confirms order
  Given I am on the Cart Details Page
  When I press the Checkout Button
  Then I should be on the Order Details Page
  When I press the Confirm Button
  Then my order should be confirmed

@loginRequired 
@cartItemsNeeded
Scenario: Check if Back button takes me back to Cart page
  Given I am on the Cart Details Page
  When I press the Checkout Button
  Then I should be on the Order Details Page
  When I press the Back Button
  Then I should be on the Cart Details Page
