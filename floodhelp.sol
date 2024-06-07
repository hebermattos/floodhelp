// SPDX-License-Identifier: MIT

pragma solidity 0.8.26;


contract FloodHelp {
    
    struct HelpRequest {
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

    uint public lastId = 0;
    mapping(uint => HelpRequest) public helpRequests;

    event HelpRequestOpened(uint id, string title, address author);
    event HelpRequestClosed(uint id, address author);
    event DonationReceived(uint id, address donor, uint amount);

    function openHelpRequest(string memory title, string memory description, string memory contact, uint goal) public {
        require(bytes(title).length > 0, "Title cannot be empty");
        require(bytes(description).length > 0, "Description cannot be empty");
        require(bytes(contact).length > 0, "Contact cannot be empty");
        require(goal > 0, "Goal must be greater than zero");

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

        emit HelpRequestOpened(lastId, title, msg.sender);
    }

    function closeHelpRequest(uint id) public {
        address author = helpRequests[id].author;
        uint balance = helpRequests[id].balance;
        uint goal = helpRequests[id].goal;

        require(helpRequests[id].open, "Help request is already closed");
        require(msg.sender == author || balance >= goal, "You cannot close this help request");

        helpRequests[id].open = false;

        if (balance > 0) {
            helpRequests[id].balance = 0;
            payable(author).transfer(balance);
        }

        emit HelpRequestClosed(id, author);
    }

    function donate(uint id) public payable {
        require(helpRequests[id].open, "Help request is closed");

        helpRequests[id].balance += msg.value;

        emit DonationReceived(id, msg.sender, msg.value);

        if (helpRequests[id].balance >= helpRequests[id].goal) {
            closeHelpRequest(id);
        }
    }

    function getHelpRequests(uint page, uint pageSize) public view returns (HelpRequest[] memory) {
        require(page > 0, "Page number must be greater than zero");
        require(pageSize > 0, "Page size must be greater than zero");

        uint start = (page - 1) * pageSize + 1;
        uint end = start + pageSize - 1;
        end = end > lastId ? lastId : end;

        HelpRequest[] memory result = new HelpRequest[](end - start + 1);
        uint resultArrayCount = 0;

        for (uint i = start; i <= end; i++) {
            result[resultArrayCount] = helpRequests[i];
            resultArrayCount++;
        }

        return result;
    }
}