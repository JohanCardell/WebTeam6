*** Settings ***
Documentation  This is a test suite that tests the Webteam6 website
Library  SeleniumLibrary
Test Setup  Begin Test
Test Teardown  End Test

*** Variables ***
${BROWSERS}                             chrome
${FIRSTNAME}                            Johan
${LASTNAME}                             Johansson
${EMAIL}                                johan.johansson@hotmail.com
${PASSWORD}                             User123!

*** Keywords ***
Begin Test
        Open Browser                    about:blank  ${BROWSERS}    options=add_argument("--ignore-certificate-errors")
        Maximize Browser Window
        Load Page
        Verify Page Loaded

Load Page
        Go To                           https://localhost:5001
        Sleep                           5s
Verify Page Loaded
        Wait Until Page Contains        RemoteTool

Given user is in main page
         Wait Until Page Contains       RemoteTool

When user clicks on element login/register
        Click Element                       id:landingLoginButton

Then Check if login page is open
        Wait Until Page Contains            Login

And user filled in valid mail
        Input Text                          id:loginEmail     ${EMAIL}

And user filled in invalid password
       Input Text                          id:loginPassword      MMK111!
	   
And clicks on element submit
        Click Element                       id:loginSubmit

Then Check if error message is displayed
       Wait Until Page Contains            Invalid login attempt.

End Test
        Close Browser

*** Test Cases ***
Login with invalid user information
    [Documentation]                     Test for user login with invalid information
    [Tags]                              Login with invalid user information_test
    Set Selenium Implicit Wait          10 seconds
    Click Element                       id:landingLoginButton
    Wait Until Page Contains            Login
    Input Text                          id:loginEmail     johan.johanssonhotmail.com
    Input Text                          id:loginPassword     MMK111!
    Click Element                       id:loginSubmit
    Wait Until Page Contains            The Email field is not a valid e-mail address.

*** Test Cases ***
Login with invalid user email
    [Documentation]                     Test for user login with invalid email
    [Tags]                              Login with invalid user email_test
    Set Selenium Implicit Wait          10 seconds
    Click Element                       id:landingLoginButton
    Wait Until Page Contains            Login
    Input Text                          id:loginEmail     johan.johanssonhotmail.com
    Input Text                          id:loginPassword     ${PASSWORD}
    Click Element                       id:loginSubmit
    Wait Until Page Contains            The Email field is not a valid e-mail address.

*** Test Cases ***
Login with invalid password
    [Documentation]                     Test for user login with invalid password
    [Tags]                              Login with invalid password_test
    Set Selenium Implicit Wait          10 seconds
    Given user is in main page
    When user clicks on element login/register
    Then Check if login page is open
    And user filled in valid mail
    And user filled in invalid password
    And clicks on element submit
    Then Check if error message is displayed

*** Test Cases ***
Login with valid user information
    [Documentation]                     Test for user login with valid username and password
    [Tags]                              Login valid user information_test
    Set Selenium Implicit Wait          10 seconds
    Click Element                       id:landingLoginButton
    Wait Until Page Contains            Login
    Input Text                          id:loginEmail     ${EMAIL}
    Input Text                          id:loginPassword     ${PASSWORD}
    Click Element                       id:loginSubmit
	Wait Until Page Contains			Welcome, ${FIRSTNAME}