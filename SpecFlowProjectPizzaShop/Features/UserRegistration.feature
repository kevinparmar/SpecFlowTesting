Feature: User Registration
  As a new user
  I want to register on the website
  So that I can access my account

  @registration
  Scenario: Successful Registration
    Given I am on the registration page
    When I enter my email
    And I enter a password
    And I confirm the password
    And I click the register button
    And I see registration confirmation message
    And I click on confirm account link
    Then I should see a confirmed email message

  Scenario: Registration with Mismatched Passwords
    Given I am on the registration page
    When I enter my email
    And I enter a password
    And I confirm with another password
    And I click the register button
    Then I should see a password mismatch error message

  Scenario: Registration with an Invalid Email
    Given I am on the registration page
    When I enter an invalid email
    And I move to the password field
    Then I should see an invalid email error message

Scenario: Registration with Existing Email
    Given I am on the registration page
    When I enter existing email
    And I enter a password
    And I confirm the password
    And I click the register button
    Then I should see a email already exists error message