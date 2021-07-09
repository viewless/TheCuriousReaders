Feature: LoginTests
Tests related to login functionality

@web
@api
Scenario: Verify successful login
	Given user is on login page
	When enter with "allison.waller@example.com" and "Password123!@#"
	Then should successfully enter to home page

@web
Scenario Outline: Verify unsuccessful login
	Given user is on login page
	When enter with "<email>" and "<password>"
	Then should receive error message "<message>"

	Examples:
		| email            | password       | message                                    |
		| t@example.com    | Password123!@# | Invalid email address or password.         |
		| admin@endava.com | pass           | Invalid email address or password.         |
		| a@example.com    | pass123        | Invalid email address or password.         |
		|                  | Password123!@# | Please enter all the required credentials. |
		| admin@endava.com |                | Please enter all the required credentials. |
		|                  |                | Please enter all the required credentials. |