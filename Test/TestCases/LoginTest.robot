*** Settings ***
Documentation  This is a test suite that tests the Webteam6 website
Library  SeleniumLibrary
Test Setup  Begin Test
Test Teardown  End Test

*** Variables ***
${BROWSERS}                             chrome
${USERNAME}                             TestUser
${EMAIL}                                testuser@hotmail.com
${PASSWORD}                             Test123!

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
        Wait Until Page Contains        Web6Team

user is in main page
         Wait Until Element Is Visible       id:userIndexPage

user clicks on element login/register
        Click Element                       id:Login/Register

Check if login page is open
        Wait Until Page Contains            Login

user filled in valid mail
        Input Text                          id:username     ${USERNAME}

user filled in invalid password
       Input Text                          id:password      MMK111!
	   
clicks on element submit
        Click Element                       id:login-submit

Check if error message is displayed
       Wait Until Page Contains            enter the correct password

End Test
        Close Browser

*** Test Cases ***
Login with valid user information
    [Documentation]                     Test for user login with valid username and password
    [Tags]                              Login_ValidTest
    Set Selenium Implicit Wait          10 seconds
    Wait Until Element Is Visible       id:userIndexPage
    Click Element                       id:Login/Register
    Wait Until Page Contains            Login
    Input Text                          id:username     ${USERNAME}
    Input Text                          id:Password     ${PASSWORD}
    Click Element                       id:login-submit
	Wait Until Page Contains			Welcome, ${USERNAME} 	


*** Test Cases ***
Login with invalid password
    [Documentation]                     Test for user login with invalid password
    [Tags]                              Login_invalidPasswordTest
    Set Selenium Implicit Wait          10 seconds
    Given       user is in main page
    When        user clicks on element login/register
    Then        Check if login page is open
    And         user filled in valid mail
    And         user filled in invalid password
    And         clicks on element submit
    Then        Check if error message is displayed



*** Test Cases ***
Login with invalid user username
    [Documentation]                     Test for user login with invalid username
    [Tags]                              Login_invalidUsernameTest
    Set Selenium Implicit Wait          10 seconds
    Wait Until Element Is Visible       id:userIndexPage
    Click Element                       id:Login/Register
    Wait Until Page Contains            Login
    Input Text                          id:username     RealTest
    Input Text                          id:password     ${PASSWORD}
    Click Element                       id:login-submit
    Wait Until Page Contains            enter the correct username