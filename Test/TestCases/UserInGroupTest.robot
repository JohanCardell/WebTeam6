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
        Wait Until Page Contains        RemoteTool

End Test
        Close Browser

*** Test Cases ***
Add, displaying, delete member in group
    [Documentation]                     Test for adding, displaying and delete member in group
    [Tags]                              MemberInGroup_test
    Set Selenium Implicit Wait          10 seconds
    Wait Until Page Contains            Welcome to RemoteTool
    Click Element                       id:landingLoginButton
    Wait Until Page Contains            Login
    Input Text                          id:loginEmail     ${EMAIL}
    Input Text                          id:loginPassword     ${PASSWORD}
    Click Element                       id:loginSubmit
    Wait Until Page Contains			Welcome, ${FIRSTNAME}
    Click Element                       xpath://td[text()="B-Team"]
    Wait Until Page Contains            B-Team
    Click Element                       id:addMember
    Wait Until Page Contains            Add Members
    Input Text                          id:searchMembers      Erik85
    Click Element                       xpath://td[text()="Erik85"]
    Click Element                       id:comfirmAddMembers
    Wait Until Page Contains            B-Team
    Table Should Contain                id:userTable    Erik85
    Click Element                       xpath://td[text()="Erik85"]/parent::tr/child::td[2]/input
    Wait Until Page Contains            B-Team
    Page Should Not Contain             Erik85
