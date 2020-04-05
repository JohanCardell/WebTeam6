*** Settings ***
Documentation  This is a test suite that tests the car booking website for Infotiv.
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
Verify Page Loaded
        Wait Until Page Contains        Hello, world!

End Test
        Close Browser

*** Test Cases ***
Navigate to Counter page
    [Documentation]             Test for accessing counter page
    [Tags]                      counter_test
    Wait Until Element Is Visible  xpath://html/body/app/div[1]/div[2]/ul/li[2]/a
    Click Element               xpath://html/body/app/div[1]/div[2]/ul/li[2]/a
    Wait Until Page Contains    Counter
    Click Element               xpath://html/body/app/div[2]/div[2]/button
    Element Should Contain      xpath://html/body/app/div[2]/div[2]/p   Current count: 1


