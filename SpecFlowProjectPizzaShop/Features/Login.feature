Feature: Login

In order to access my account 
As a registered user
I want to be able to log in to the application

@login
Scenario: Successful Login 
	Given I am on the login page
	When I enter my username and password
	And I click on login button
	Then I should be logged in successfully

@login-fail
Scenario: Failed login with invalid credentials
    Given I am on the login page
    When I enter invalid username and password
    And I click the login button
    Then I should see an error message
