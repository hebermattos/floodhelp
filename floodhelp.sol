// SPDX-License-Identifier: GPL-3.0

pragma solidity >=0.7.0 <0.9.0;

struct HelpRequest{

    uint id;
    string title;
    string description;
    string contact;
    uint timestamp;
    address author;
    uint goal;
    uint balance;
    bool open;

}

contract FloodHelp {

    uint public lastId = 0;
    mapping(uint => HelpRequest) public helpRequests;

    function openHelpRequest(string memory title, string memory description, string memory contact, uint goal) public{

        lastId++;

        helpRequests[lastId] = HelpRequest({
            id: lastId,
            title: title,
            description: description,
            contact: contact,
            goal: goal,
            balance: 0,
            open: true,
            timestamp: block.timestamp,
            author: msg.sender
        });
    }

    function closeHelpRequest(uint id) public {
        address author = helpRequests[id].author;
        uint balance = helpRequests[id].balance;
        uint goal = helpRequests[id].goal;

        require(helpRequests[id].open && (msg.sender == author || balance >= goal), "You cannot close this help request");

        helpRequests[id].open = false;

        if(balance > 0){
            helpRequests[id].balance = 0;
            payable(author).transfer(balance);
        } 
    }

    function donate(uint id) public payable{
        helpRequests[id].balance += msg.value;

        if(helpRequests[id].balance >= helpRequests[id].goal)  
            closeHelpRequest(id);
    }

    function getHelpRequests(uint page, uint pageSize) public view returns (HelpRequest[] memory){

        HelpRequest[] memory result = new HelpRequest[](pageSize);

        uint resultArrayCount = 0;
        uint HelpRequestPosition = ((page - 1) * pageSize) + 1;

        do {
            result[resultArrayCount] = helpRequests[HelpRequestPosition];
            HelpRequestPosition++;
            resultArrayCount++;           
        }
        while (resultArrayCount < pageSize && HelpRequestPosition <= lastId);
    
        return result;
    }
    
}