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
    [Tags]                      event_test
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
    Input Text                  id:addEventName  testEvent
    Input Text                  id:addEventDate  2021-04-28
    Input Text                  id:addEventDesc  This is a test desciption.
    Click Element               id:addEventButton
    Wait Until Page Contains    testEvent
    Table Should Contain        id:eventTable    testEvent
    Table Should Contain        id:eventTable    2021-04-28
    Table Should Contain        id:eventTable    This is a test desciption.


Modify and remove test event
    [Documentation]             Test for accessing and testing events
    [Tags]                      event_test
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
    Wait Until Page Contains    testEvent
    Table Should Contain        id:eventTable    testEvent
    Table Should Contain        id:eventTable    2021-04-28
    Table Should Contain        id:eventTable    This is a test desciption.
    ${EDITBUTTON}  Get Webelement     xpath://td[text()="testEvent"]/parent::tr/child::td[3]/input
    Click Element                ${EDITBUTTON}
    Wait until Page Contains     Edit testEvent
    Input Text                   id:editEventName   new textEvent
    Input Text                  id:editEventDate  2021-03-25
    Input Text                  id:editEventDesc  This is a new test desciption.
    Click Element                id:editButton
    Wait Until Page Contains    new textEvent
    Table Should Contain        id:eventTable    new textEvent
    Table Should Contain        id:eventTable    2021-03-25
    Table Should Contain        id:eventTable    This is a new test desciption.
    ${DELETEBUTTON}  Get Webelement     xpath://td[text()="new textEvent"]/parent::tr/child::td[3]/input
    Click Element               ${DELETEBUTTON}
    Page Should Not Contain     new textEvent

