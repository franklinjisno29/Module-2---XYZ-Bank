Feature: Deposit Amount


@E2E-Deposit_Amount
Scenario: Deposit Amount
	Given User will be on Homepage
	When User will click on the Customer Login Button
	Then Customer Page is loaded in the same page
	When User selects a '<customerno>' from the list
	* Clicks the Login Button
	Then Account Page is loaded in the same page
	When User select the '<accountno>'
	* Clicks the Deposit select Button
	* Types the '<amount>' to Deposit
	* Clicks the Deposit Button
	Then Success Message Appears

Examples:
	| accountno   | customerno | amount |
	| number:1004 | 2          | 1000   |



