*** Settings ***
Documentation  This is a test suite that tests the Webteam6 website
Library  SeleniumLibrary
Test Setup  Begin Test
Test Teardown  End Test

*** Variables ***
${BROWSERS}           chrome

*** Keywords ***
Begin Test
        Open Browser                about:blank  ${BROWSERS}    options=add_argument("--ignore-certificate-errors")
        Maximize Browser Window
        Load Page
        Verify Page Loaded

Load Page
        Go To                       https://localhost:5001
        Sleep                       5s
Verify Page Loaded
        Wait Until Page Contains        Hello, world!

End Test
        Close Browser

*** Test Cases ***
Create, verify and delete user
    [Documentation]             Test for create, verify and delete user account
    [Tags]                      register_test
    Set Selenium Implicit Wait      10 seconds
    Wait Until Element Is Visible  id:userIndexPage
    Click Element               id:userIndexPage
    Wait Until Page Contains    Shows all users in database
    Click Element               id:add-user-btn
    Wait Until Page Contains    User details
    Sleep                       3s
    Input Text                  id:new-username-field  User1
    Click Element               id:new-email-field
    Input Text                  id:new-email-field      mohammedtikabo@outlook.com
    Input Text                   id:new-password-field      Mt091113
    Click Element                   id:submit-btn
    Wait Until Page Contains         Shows all users in database
    Element Text Should Be          xpath://html/body/app/div[2]/div[2]/table/tbody/tr/td[1]       User1
    Element Text Should Be          xpath://html/body/app/div[2]/div[2]/table/tbody/tr/td[2]       mohammedtikabo@outlook.com
    Click Element                   xpath://html/body/app/div[2]/div[2]/table/tbody/tr/td[4]/input