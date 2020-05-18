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

user is in main page
         Wait Until Element Is Visible       Welcome to RemoteTool

user clicks on element login/register
        Click Element                       id:landingLoginButton

Check if login page is open
        Wait Until Page Contains            Login

user filled in valid mail
        Input Text                          id:loginEmail     ${EMAIL}

user filled in invalid password
       Input Text                          id:password      MMK111!
	   
clicks on element submit
        Click Element                       id:loginSubmit

Check if error message is displayed
       Wait Until Page Contains            enter the correct password

End Test
        Close Browser

*** Test Cases ***
Login with invalid user information
    [Documentation]                     Test for user login with invalid information
    [Tags]                              Login with invalid user information_test
    Set Selenium Implicit Wait          10 seconds
    Wait Until Element Is Visible       Welcome to RemoteTool
    Click Element                       id:landingLoginButton
    Wait Until Page Contains            Login
    Input Text                          id:loginEmail     johan.johanssonhotmail.com
    Input Text                          id:loginPassword     MMK111!
    Click Element                       id:loginSubmit
    Wait Until Page Contains            enter the correct information

*** Test Cases ***
Login with invalid user email
    [Documentation]                     Test for user login with invalid email
    [Tags]                              Login with invalid user email_test
    Set Selenium Implicit Wait          10 seconds
    Wait Until Element Is Visible       Welcome to RemoteTool
    Click Element                       id:landingLoginButton
    Wait Until Page Contains            Login
    Input Text                          id:loginEmail     johan.johanssonhotmail.com
    Input Text                          id:loginPassword     ${PASSWORD}
    Click Element                       id:loginSubmit
    Wait Until Page Contains            enter the correct email

*** Test Cases ***
Login with invalid password
    [Documentation]                     Test for user login with invalid password
    [Tags]                              Login with invalid password_test
    Set Selenium Implicit Wait          10 seconds
    Given       user is in main page
    When        user clicks on element login/register
    Then        Check if login page is open
    And         user filled in valid mail
    And         user filled in invalid password
    And         clicks on element submit
    Then        Check if error message is displayed

*** Test Cases ***
Login with valid user information
    [Documentation]                     Test for user login with valid username and password
    [Tags]                              Login valid user information_test
    Set Selenium Implicit Wait          10 seconds
    Wait Until Element Is Visible       Welcome to RemoteTool
    Click Element                       id:landingLoginButton
    Wait Until Page Contains            Login
    Input Text                          id:loginEmail     ${EMAIL}
    Input Text                          id:loginPassword     ${PASSWORD}
    Click Element                       id:loginSubmit
	Wait Until Page Contains			Welcome, ${FIRSTNAME}