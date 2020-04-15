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
Navigate to Register page
    [Documentation]             Test for accessing register page
    [Tags]                      register_test
    Set Selenium Implicit Wait      10 seconds
    Wait Until Element Is Visible  id:userRegPage
    Click Element               id:userRegPage
    Wait Until Page Contains    Register Account
    Input Text                  id:usernameRegInput  User1
    Textfield Value Should Be     id:usernameRegInput   User1
    Input Text                     id:emailRegInput   mohammedtikabo@outlook.com
    Textfield Value Should Be       id:emailRegInput   mohammedtikabo@outlook.com

