*** Settings ***
Documentation  This is a test suite that tests the Webteam6 website
Library  SeleniumLibrary
Test Setup  Begin Test
Test Teardown  End Test

*** Variables ***
${BROWSER}        chrome

*** Keywords ***
Begin Test
        Open Browser                about:blank  ${BROWSER}     options=add_argument("--ignore-certificate-errors")
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
    Set Selenium Implicit Wait      20 seconds
    Wait Until Element Is Visible  id:userIndexPage
    Click Element               id:userIndexPage
    Wait Until Page Contains     Shows all users in database
    Click Button                   id:add-user-btn
    Wait Until Page Contains        User details
    Input Text                  id:new-username-field  User1
    Textfield Value Should Be     id:new-username-field  User1
    Input Text                     id:new-email-field   mohammedtikabo@outlook.com
    Textfield Value Should Be       id:new-email-field   mohammedtikabo@outlook.com
    Input Text                      id:new-password-field      Mt091113
    Textfield Value Should Be       id:new-password-field      Mt091113
    Click Button                   id:submit-btn
    Sleep                           10s
    Element Text Should Be          xpath://html/body/app/div[2]/div[2]/table/tbody/tr/td[1]        User1
    Element Text Should Be          xpath://html/body/app/div[2]/div[2]/table/tbody/tr/td[2]        mohammedtikabo@outlook.com
    Page Should Contain Button       xpath://*[@id="user-delete-btn "]
    Click Button                    xpath://*[@id="user-delete-btn "]