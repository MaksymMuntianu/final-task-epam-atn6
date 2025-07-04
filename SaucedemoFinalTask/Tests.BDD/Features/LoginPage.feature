Feature: LoginPage

As a user
I want to log in to the application
So that I can see the inventory page

Background: 
	Given I am on the login page

@smoke
Scenario: Show error message when log in with empty credentials
	When I enter "standard_user" in the username, and "secret_sauce" in the password fields
	And I clear the username and password fields
	And I click the login button
	Then I should see an error message that contains "Username is required"

@smoke
Scenario: Show error message when log in with empty "Password" field
	When I enter "standard_user" in the username, and "secret_sauce" in the password fields
	And I clear the password field
	And I click the login button
	Then I should see an error message that contains "Password is required"

@smoke
Scenario Outline: Successful login with valid credentials
	When I enter valid <username> in the username, and valid <password> in password fields
	And I click the login button
	Then I should see the inventory page with the title "Swag Labs" in the dashboard

	# Excluded locked_out_user because it is not a valid test case for successful login,
    # and performance_glitch_user to speed up the test suite.
	Examples: 
		| username                | password     |
		| standard_user           | secret_sauce |
		| error_user              | secret_sauce |
		| performance_glitch_user | secret_sauce |
		| visual_user             | secret_sauce |
