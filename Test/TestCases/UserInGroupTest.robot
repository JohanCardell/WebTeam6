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
Add, displaying, delete user in group
    [Documentation]                     Test for adding, displaying and delete user in group
    [Tags]                              UserInGroup_test
    Set Selenium Implicit Wait          10 seconds
    Wait Until Element Is Visible       id:login/register
    Click Element                       id:Login/register
    Wait Until Page Contains            Login
    Input Text                          id:username     ${USERNAME}
    Input Text                          id:Password     ${PASSWORD}
    Click Element                       id:login-submit
    Wait Until Page Contains			Welcome, ${USERNAME}
    Click Element                       id:C-Team
    Wait Until Page Contains            C-Team
    Click Element                       id:add user
    Wait Until Page Contains            Add User
    Input Text                          id:add username      Erik85
    Click Element                       id:confirm add user
    Wait Until Page Contains            C-Team
    Page Should Contain Element         Erik85

