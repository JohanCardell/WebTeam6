*** Settings ***

Documentation  This is a test suite that tests the Webteam6 website
Library  SeleniumLibrary
Test Setup  Begin Test
Test Teardown  End Test

*** Variables ***
${BROWSERS}                             chrome
${USERNAME}                             User1
${FIRSTNAME}                            Johan
${LASTNAME}                             Johansson
${EMAIL}                                Johan.johansson@hotmail.com
${PASSWORD}                             User123!

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
        Wait Until Page Contains        Web6Team

End Test
        Close Browser
*** Test Cases ***
Register, verify and delete user
    [Documentation]                     Test for register, verify and delete user account
    [Tags]                              register_test
    Set Selenium Implicit Wait          10 seconds
    Wait Until Element Is Visible       id:login/register
    Click Element                       id:Login/register

  