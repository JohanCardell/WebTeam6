*** Settings ***
Documentation  This is a test suite that tests the Webteam6 website
Library  SeleniumLibrary
Test Setup  Begin Test
Test Teardown  End Test

*** Variables ***
${BROWSERS}                             chrome
${USERNAME}                             User1
${FIRSTNAME}                            Johan
${LASTNAME}                             Johansson
${EMAIL}                                Johan.johansson@hotmail.com
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
        Wait Until Page Contains        Web6Team

End Test
        Close Browser
*** Test Cases ***
Register, verify and delete user
    [Documentation]                     Test for register, verify and delete user account
    [Tags]                              register_test
    Set Selenium Implicit Wait          10 seconds
    Wait Until Element Is Visible       id:login/register
    Click Element                       id:Login/register
    Wait Until Page Contains            Register
    Input Text                          id:new-username-field  ${USERNAME}
    Input Text                          id:new-email-field      ${EMAIL}
    Input Text                          id:fristname        ${FIRSTNAME}
    Input Text                          id:lastname         ${LASTNAME}
    Input Text                          id:new-password-field     ${PASSWORD}
    Input Text                          id:comfirm password       ${PASSWORD}
    Click Element                       id:register
    Wait Until Page Contains            Welcome, ${FIRSTNAME}
    Click Element                       id:account settings
    Wait Until Page Contains            Account settings
    Click Element                       delete account
    Wait Until Page Contains            account is deleted


*** Test Cases ***
Register user with invalid email account
    [Documentation]                     Test for rgister user with invalid email account
    [Tags]                              register_InvalidEmailAccountTest
    Set Selenium Implicit Wait          10 seconds
    Wait Until Element Is Visible       id:login/register
    Click Element                       id:Login/register
    Wait Until Page Contains            Register
    Input Text                          id:new-username-field  ${USERNAME}
    Input Text                          id:new-email-field      Johan.johanssonhotmail.com
    Input Text                          id:fristname        ${FIRSTNAME}
    Input Text                          id:lastname         ${LASTNAME}
    Input Text                          id:new-password-field     ${PASSWORD}
    Input Text                          id:comfirm password       ${PASSWORD}
    Click Element                       id:register
    Wait Until Page Contains            error messege, enter the correct email accont

