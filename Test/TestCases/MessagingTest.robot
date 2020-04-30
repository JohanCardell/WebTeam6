*** Settings ***
Documentation  This is a test suite that tests the Webteam6 website
Library  SeleniumLibrary
Test Setup  Begin Test
Test Teardown  End Test

*** Variables ***
${BROWSERS}                             chrome
${USERNAME}                             User1
${EMAIL}                                mohammedtikabo@outlook.com
${PASSWORD}                             Mt091113

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
        Wait Until Page Contains        Hello, world!

End Test
        Close Browser
*** Test Cases ***
Create, verify and delete user
    [Documentation]                     Test for create, verify and delete user account
    [Tags]                              register_test
    Set Selenium Implicit Wait          10 seconds
    Wait Until Element Is Visible       id:userIndexPage
    Click Element                       id:userIndexPage
    Wait Until Page Contains            Shows all users in database
    Click Element                       id:add-user-btn
    Wait Until Page Contains            User details
    Sleep                               3s
    Input Text                          id:new-username-field  ${USERNAME}
    Input Text                          id:new-email-field      ${EMAIL}
    Input Text                          id:new-password-field     ${PASSWORD}
    Click Element                       id:submit-btn
    Wait Until Page Contains            ${USERNAME}
    Table Should Contain                xpath://html/body/app/div[2]/div[2]/table   ${USERNAME}
    Table Should Contain                xpath://html/body/app/div[2]/div[2]/table   ${EMAIL}
    ${DELETEBUTTON}  Get Webelement     xpath://td[text()="${USERNAME}"]/parent::tr/child::td[4]/input
    Click Element                       ${DELETEBUTTON}
