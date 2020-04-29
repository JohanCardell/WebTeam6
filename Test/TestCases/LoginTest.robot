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
Login
    [Documentation]                     Test for user login
    [Tags]                              login_test
    Set Selenium Implicit Wait          10 seconds
    Wait Until Element Is Visible       id:userIndexPage
    Click Element                       xpath://html/body/app/div[2]/div[1]/a[2]
    Wait Until Page Contains            Log in
    Input Text                          id:Input_Email      ${EMAIL}
    Input Text                          id:Input_Password     ${PASSWORD}
    Click Element                       id:login-submit