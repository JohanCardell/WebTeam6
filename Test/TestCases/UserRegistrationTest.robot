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
    Wait Until Element Is Visible  id:userRegPage
    Click Element               id:userRegPage
    Wait Until Page Contains    Register Account
    Input Text                  id:usernameRegInput  User1
    Textfield Value Should Be     id:usernameRegInput   User1
    Input Text                     id:emailRegInput   mohammedtikabo@outlook.com
    Textfield Value Should Be       id:emailRegInput   mohammedtikabo@outlook.com
    Input Text                      id:passwordRegInput      Mt091113
    Textfield Value Should Be       id:passwordRegInput      Mt091113
    Click Button                   id:regUserButton
    Sleep                           10s
    Click Element                   xpath://*[@id="userRegPage"][2]
    Sleep                           10s
    Wait Until Page Contains        User name
    Element Text Should Be          xpath://html/body/app/div[2]/div[2]/table/tbody/tr/td[1]        User1
    Element Text Should Be          xpath://html/body/app/div[2]/div[2]/table/tbody/tr/td[2]        mohammedtikabo@outlook.com
    Click Element                   xpath://html/body/app/div[2]/div[2]/table/tbody/tr/td[4]/input