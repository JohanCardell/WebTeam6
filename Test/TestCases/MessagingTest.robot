*** Settings ***
Documentation  This is a test suite that tests the event feature
Library  SeleniumLibrary
Test Setup  Begin Test
Test Teardown  End Test

*** Variables ***
${BROWSER}          chrome
${EMAIL}            johan.johansson@hotmail.com
${PASSWORD}         User123!
${FIRSTNAME}        Johan
${LASTNAME}         Johansson
*** Keywords ***
Begin Test
        Open Browser                about:blank  ${BROWSER}     options=add_argument("--ignore-certificate-errors")
        Maximize Browser Window
        Load Page
        Verify Page Loaded

Load Page
        Go To                       https://localhost:5001
Verify Page Loaded
        Wait Until Page Contains        RemoteTool

End Test
        Close Browser

*** Test Cases ***
Create and view messages
    [Documentation]             Test for accessing and testing messages in group
    [Tags]                      messaging_test
    Click Element               id:landingLoginButton
    Wait Until Page Contains    Login
    Input Text                  id:loginEmail    ${EMAIL}
    Input Password              id:loginPassword    ${PASSWORD}
    Click Element               id:loginSubmit
    Wait Until Page Contains    Welcome, ${FIRSTNAME}
    Wait Until Element Exists   xpath://li[text()="testGroup"]
    Click Element               xpath://li[text()="testGroup"]
    Wait Until Page Contains    testGroup
    Input Text                  id:messageField   This is a test message.
    Click Element               id:messageSend
    Wait Until Page Contains    This is a test message.

