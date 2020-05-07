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
Create group and rename it
    [Documentation]                     Test for creating a group and rename it
    [Tags]                              createGroup_test
    Set Selenium Implicit Wait          10 seconds
    Wait Until Element Is Visible       id:login/register
    Click Element                       id:Login/register
    Wait Until Page Contains            Login
    Input Text                          id:username     ${USERNAME}
    Input Text                          id:Password     ${PASSWORD}
    Click Element                       id:login-submit
    Wait Until Page Contains			Welcome, ${USERNAME}
    Click Element                       id:create group
    Wait Until Page Contains            Group Name
    Click Element                       id:group name
    Input Text                          id:nameforgroup    A-Team
    Wait Until Page Contains            A-Team
    Click Element                       id:group name
    Input Text                          id:nameforgroup    B-Team
    Wait Until Page Contains            B-Team
    Click Element                       id:group settings
    Wait Until Page Contains            Group settings
    Input Text                          id:name_group       C-Team
    Click Element                       id:confirm
    Wait Until Page Contains            C-Team

*** Test Cases ***

Delete Group
    [Documentation]                     Test for delete group
    [Tags]                              deleteGroup_test
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
    Click Element                       id:group settings
    Wait Until Page Contains            Group settings
    Click Element                       id:remove group
    Wait Until Page Contains            remove group
    Click Element                       id:remove confirm
    Wait Until Page Contains            Welcome, ${USERNAME}
    Page Should Not Contain Element     id:C-Team



