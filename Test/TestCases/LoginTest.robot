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
        Wait Until Page Contains        Hello, world!

user is in main page
         Wait Until Element Is Visible       id:userIndexPage

user ckliks on element login/register
        Click Element                       id:Login/Register

page sould contains
        Wait Until Page Contains            Login

user filled in valid mail as
        Input Text                          id:username     ${USERNAME}

user filled in invalid password
       Input Text                          id:Password      MMK111!
clicks on element submit
        Click Element                       id:login-submit

page should contains
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


*** Test Cases ***
Login with invalid password
    [Documentation]                     Test for user login with invalid password
    [Tags]                              Login_invalidPasswordTest
    Set Selenium Implicit Wait          10 seconds
    Given       user is in main page
    When        user ckliks on element login/register
    Then        page sould contains
    And         user filled in valid mail as
    And         user filled in invalid password
    And         clicks on element submit
    Then        page should contains



*** Test Cases ***
Login with invalid user username
    [Documentation]                     Test for user login with invalid username
    [Tags]                              Login_invalidUsernameTest
    Set Selenium Implicit Wait          10 seconds
    Wait Until Element Is Visible       id:userIndexPage
    Click Element                       id:Login/Register
    Wait Until Page Contains            Login
    Input Text                          id:username     RealTest
    Input Text                          id:Password     ${PASSWORD}
    Click Element                       id:login-submit
    Wait Until Page Contains            enter the correct username