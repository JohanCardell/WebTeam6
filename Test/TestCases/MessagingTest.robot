*** Settings ***
Documentation  This is a test suite that tests the event feature
Library  SeleniumLibrary
Test Setup  Begin Test
Test Teardown  End Test

*** Variables ***
${BROWSER}        chrome
${USERNAME}       User1
${PASSWORD}       Password1
*** Keywords ***
Begin Test
        Open Browser                about:blank  ${BROWSER}     options=add_argument("--ignore-certificate-errors")
        Maximize Browser Window
        Load Page
        Verify Page Loaded

Load Page
        Go To                       https://localhost:5001
Verify Page Loaded
        Wait Until Page Contains        Hello, world!

End Test
        Close Browser

*** Test Cases ***
Create and view test event
    [Documentation]             Test for accessing and testing events
    [Tags]                      messaging_test
    Wait Until Page Contains    Login/Register
    Click Element               id:loginButton
    Wait Until Page Contains    Login
    Input Text                  id:loginUsername    ${USERNAME}
    Input Password              id:loginPassword    ${PASSWORD}
    Click Element               id:loginButton
    Wait Until Page Contains    Welcome, User1
    Wait Until Element Exists   xpath://li[@id="testGroup"]
    Click Element               xpath://li[@id="groupCreatePage"]
    Wait Until Page Contains    testGroup
    Input Text                  id:messageBox   This is a test message.
    Click Element               id:messageSend
    Wait Until Page Contains    This is a test message.

