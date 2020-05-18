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
        Wait Until Page Contains        RmoteTool

End Test
        Close Browser
*** Test Cases ***
Create group and rename it
    [Documentation]                     Test for creating a group and rename it
    [Tags]                              createGroup_test
    Set Selenium Implicit Wait          10 seconds
    Wait Until Page Contains            Welcome to RemoteTool
    Click Element                       id:landingLoginButton
    Wait Until Page Contains            Login
    Input Text                          id:loginEmail     ${EMAIL}}
    Input Text                          id:loginPassword     ${PASSWORD}
    Click Element                       id:loginSubmit
    Wait Until Page Contains			Welcome, ${FIRSTNAME}}
    Click Element                       id:createGroup
    Wait Until Page Contains            Group Name
    Click Element                       id:groupName
    Input Text                          id:nameforgroup    A-Team
    Wait Until Page Contains            A-Team
    Click Element                       id:changeGroupName
    Wait Until Page Contains            Change Group Name
    Input Text                          id:groupNameField    B-Team
    Click Element                       id:confirmChangeName
    Wait Until Page Contains            B-Team


*** Test Cases ***

Delete Group
    [Documentation]                     Test for delete group
    [Tags]                              deleteGroup_test
    Set Selenium Implicit Wait          10 seconds
    Wait Until Page Contains            Welcome to RemoteTool
    Click Element                       id:landingLoginButton
    Wait Until Page Contains            Login
    Input Text                          id:loginEmail     ${EMAIL}
    Input Text                          id:loginPassword     ${PASSWORD}
    Click Element                       id:loginSubmit
    Wait Until Page Contains			Welcome, ${FIRSTNAME}
    Click Element                       id:groupName B-Team
    Wait Until Page Contains            B-Team
    Click Element                       id:deleteGroup
    Wait Until Page Contains            Delete B-Team Group
    Click Element                       id:comfirmDeleteGroup
    Wait Until Page Contains            Welcome, ${FIRSTNAME}
    Page Should Not Contain Element     id:groupName B-Team



