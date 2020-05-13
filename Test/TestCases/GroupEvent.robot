*** Settings ***
Documentation  This is a test suite that tests the event feature
Library  SeleniumLibrary
Test Setup  Begin Test
Test Teardown  End Test

*** Variables ***
${BROWSER}        chrome
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
Create and view test event
    [Documentation]             Test for accessing and testing events
    [Tags]                      event_test
    Click Element               id:ladingLoginButton
    Wait Until Page Contains    Login
    Input Text                  id:loginUsername    ${EMAIL}
    Input Password              id:loginPassword    ${PASSWORD}
    Click Element               id:loginButton
    Wait Until Page Contains    Welcome, ${FIRSTNAME}
    Wait Until Element Exists   xpath://li[text()="testGroup"]
    Click Element               xpath://li[text()="testGroup"]
    Wait Until Page Contains    testGroup
    Input Text                  id:eventNameField  testEvent
    Input Text                  id:eventStartField  2021-04-28
    Input Text                  id:eventEndField  2021-04-28
    Input Text                  id:eventDescField  This is a test desciption.
    Click Element               id:eventAdd
    Wait Until Page Contains    testEvent
    Table Should Contain        id:eventTable    testEvent
    Table Should Contain        id:eventTable    2021-04-28
    Table Should Contain        id:eventTable    This is a test desciption.


Modify and remove test event
    [Documentation]             Test for accessing and testing events
    [Tags]                      event_test
    Click Element               id:ladingLoginButton
    Wait Until Page Contains    Login
    Input Text                  id:loginUsername    ${EMAIL}
    Input Password              id:loginPassword    ${PASSWORD}
    Click Element               id:loginButton
    Wait Until Page Contains    Welcome, ${FIRSTNAME}
    Wait Until Element Exists   xpath://li[text()="testGroup"]
    Click Element               xpath://li[text()="testGroup"]
    Wait Until Page Contains    testGroup
    Wait Until Page Contains    testEvent
    Table Should Contain        id:eventTable    testEvent
    Table Should Contain        id:eventTable    2021-04-28
    Table Should Contain        id:eventTable    This is a test desciption.
    ${EDITBUTTON}  Get Webelement     xpath://td[text()="testEvent"]/parent::tr/child::td[3]/input
    Click Element                ${EDITBUTTON}
    Wait until Page Contains     Edit testEvent
    Input Text                   id:editEventName   new textEvent
    Input Text                  id:editEventStartDate  2021-03-25
    Input Text                  id:editEventEndDate  2021-03-25
    Input Text                  id:editEventDesc  This is a new test desciption.
    Click Element               id:editButton
    Wait Until Page Contains    new textEvent
    Table Should Contain        id:eventTable    new textEvent
    Table Should Contain        id:eventTable    2021-03-25
    Table Should Contain        id:eventTable    This is a new test desciption.
    ${DELETEBUTTON}  Get Webelement     xpath://td[text()="new textEvent"]/parent::tr/child::td[4]/input
    Click Element               ${DELETEBUTTON}
    Page Should Not Contain     new textEvent

