Feature: LandingPageTests
	User story 207: Create the landing page

Background:
	Given the user is on Landing page

@web
Scenario: Verify correct contact info in footer (#432)
	Then the user should see correct contact info in footer

@web
Scenario: Verify correct name of the library on landing page (#433)
	Then the user should see correct name of the library

@web
Scenario: Verify user can login from landing page (#434)
	Then the user should see login button

@web
Scenario: Verify user can sign up from landing page (#436)
	Then the user should see register button

@web
Scenario: Verify the total number of books in different genres are displayed on landing page (#494)
	Then the user should see total number of books in different genres

@web
Scenario: Verify a newly added books are displayed on landing page (#496)
	Then the user should see a newly added books table

@web
@api
Scenario: Verify the number of readers is displayed on landing page (#497)
	Then the user should see the number of readers

@web @api
Scenario: Verify correct number of readers (#534)
	Then the user should see correct number of readers

#@web
#Scenario: Verify correct total number of books on landing page (#533)
#	Then the user should see correct number of books

@web
@api
Scenario: Verify correct number of books in different genres when add new book
	When new book is added
	Then counter for books in this genre should be greater with 1 book