using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace CybersecurityChatbotGUI
{
    // === MANDATORY PART 2 LEARNING UNIT: DELEGATE DEFINITION ===
    // This satisfies the "Use delegates to solve a programming problem" requirement.
    public delegate void BotResponseDelegate(string text, string hexColor);

    public partial class MainWindow : Window
    {
        private Random random = new Random();

        // --- STATE MANAGEMENT ---
        private string userName = "";
        private string favoriteTopic = ""; // Tracks user's favorite topic for Memory & Recall
        private string lastRecognizedKeyword = "";
        private bool isFirstMessage = true;

        // Delegate Instance used for processing message output streams
        private BotResponseDelegate responseWriter;

        // --- EXPANDED GENERIC DICTIONARY (All 15 Integrated Awareness Topics) ---
        private Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            { "password", new List<string> {
                "Use distinct passphrases of 4+ random words. Avoid reuse across banking and corporate accounts.",
                "Consider utilizing a cryptographic Password Manager to completely mitigate password fatigue safety risks.",
                "Never store plain text passwords in local system documents or browser autocomplete vaults."
            }},
            { "scam", new List<string> {
                "Scammers exploit social compliance and urgency. Treat unscheduled urgent texts claiming to be from executives or banks as suspicious.",
                "If someone requests digital payment or gift cards to settle an emergency invoice, it's a definitive scam marker.",
                "Financial organizations will never request verification pins or credentials through an SMS interaction."
            }},
            { "privacy", new List<string> {
                "Minimize your digital blueprint footprint by locking down public-facing profile metadata on networking platforms.",
                "Review active connection tokens and permissions granted to third-party integrated apps periodically.",
                "Data brokers scrape open platforms. Avoid answering public survey trends that mirror common security questions."
            }},
            { "phishing", new List<string> {
                "Inspect incoming email header domains carefully. Attackers substitute characters like replacing 'm' with 'rn'.",
                "Do not interact with attachments containing executable scripts (.vbs, .exe, .scr) or macros inside spreadsheets.",
                "Phishing strategies pivot heavily around panic metrics. When in doubt, verify the alert via an independent alternate channel."
            }},
            { "browsing", new List<string> {
                "Verify the connection relies on HTTPS encryption, though note modern adversaries deploy valid SSL certificates on malicious sites too.",
                "Incorporate network-level content filters or browser extensions to block known malicious script tracking nodes.",
                "Typosquatting redirects users to spoofed domains. Carefully double-check URLs before entering identity forms."
            }},
            { "mfa", new List<string> {
                "Implement Time-Based One-Time Passwords (TOTP) or physical hardware keys rather than reliance on standard SMS authentication.",
                "Treat unsolicited MFA authentication push alerts as a compromise indicator. Change credentials immediately.",
                "Multi-factor authentication adds a secondary validation barrier, significantly lowering unauthorized access metrics."
            }},
            { "social", new List<string> {
                "Pretexting involves crafting false scenarios. Be skeptical of unverified callers claiming inner organizational credentials.",
                "Authority principles are heavily weaponized by social engineers. Confirm executive demands directly through corporate systems.",
                "Baiting relies on human curiosity. Never interface unknown hardware drives found in public facilities."
            }},
            { "wifi", new List<string> {
                "Public wireless ecosystems present adversary-in-the-middle risks where attackers capture unencrypted data streams.",
                "Always route traffic through an encrypted VPN tunnel whenever interfacing public municipal hot spots.",
                "Disable automatic network connectivity preferences on mobile endpoints to prevent rogue access point attachments."
            }},
            { "backup", new List<string> {
                "Enforce the 3-2-1 layout framework: Keep 3 copies of production systems across 2 unique media types with 1 copy fully offline.",
                "Test system file restoration pipelines regularly to confirm archive continuity integrity during data corruption scenarios.",
                "Isolated offline storage architectures are your ultimate leverage asset against enterprise system lockouts."
            }},
            { "updates", new List<string> {
                "Software patches address publicly disclosed security flaws. Prompt deployment closes entry gates for automatic scripts.",
                "Configure automatic configuration updates on operating systems and critical security client profiles.",
                "Legacy software configurations reaching End-of-Life (EOL) status are open vulnerabilities and should be phased out."
            }},
            { "mobile", new List<string> {
                "Only fetch endpoint software from official distribution marketplaces to bypass sideloaded malware variants.",
                "Audit app manifest requests; a basic utility tool has no functional business requirement accessing geo-location maps.",
                "Keep mobile baseband and firmware packages fully patched to counter advanced remote execution exploits."
            }},
            { "socialmedia", new List<string> {
                "Oversharing your corporate location assets exposes background structural architecture details to targeting actors.",
                "Limit visibility controls strictly to authentic family structures to minimize identity harvesting exposure risks.",
                "Be cautious of connection invitations from newly generated profiles claiming overlapping acquaintance linkages."
            }},
            { "physical", new List<string> {
                "Tailgating bypasses perimeter card systems. Ensure unknown persons don't slip through open doors behind you.",
                "Enforce a strict clean-desk configuration workspace: lock up sensitive corporate documentation when moving away.",
                "Discard critical identifying data files inside authorized cross-shred bins rather than open disposal units."
            }},
            { "ransomware", new List<string> {
                "Ransomware threat actors map internal systems before deploying payloads to maximize cross-network operational impacts.",
                "Isolate compromised endpoints instantly from your physical switch to prevent lateral propagation across subnets.",
                "Paying cybercriminals rarely guarantees clean decryption assets and labels your organization an easy ongoing target."
            }},
            { "iot", new List<string> {
                "Isolate smart appliances onto an independent network segment separate from your core desktop equipment hardware.",
                "Overturn default administration access configurations during out-of-box initial deployment tasks.",
                "Firmware configurations on low-tier smart consumer accessories are rarely updated, rendering them high risk vectors."
            }}
        };

        private Dictionary<string, string> sentimentEmpathy = new Dictionary<string, string>()
        {
            { "worried", "Bot: Security hazards are stressful, but implementing structural defensive controls removes the risk. " },
            { "curious", "Bot: Expanding your tactical technical awareness is the single strongest armor you can build. " },
            { "frustrated", "Bot: Defenses can feel rigorous, but operational resilience requires persistent vigilance. " }
        };

        public MainWindow()
        {
            InitializeComponent();
            
            // Wire up the delegate instance to point to our UI writing method
            responseWriter = new BotResponseDelegate(AppendColorText);

            // --- TRANSLATE TASK 1 ASCII ART INTO THE GUI ---
            string asciiArtBanner =
                @"
 ██████╗██╗   ██╗██████╗ ███████╗██████╗     ███████╗███████╗ ██████╗██╗   ██╗██████╗ ██╗████████╗██╗   ██╗     
██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗    ██╔════╝██╔════╝██╔════╝██║   ██║██╔══██╗██║╚══██╔══╝╚██╗ ██╔╝     
██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝    ███████╗█████╗  ██║     ██║   ██║██████╔╝██║   ██║    ╚████╔╝      
██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗    ╚════██║██╔══╝  ██║     ██║   ██║██╔══██╗██║   ██║     ╚██╔╝       
╚██████╗   ██║   ██████╔╝███████╗██║  ██║    ███████║███████╗╚██████╗╚██████╔╝██║  ██║██║   ██║      ██║        
 ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝    ╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═╝╚═╝   ╚═╝      ╚═╝        
" + "\n";
            responseWriter(asciiArtBanner, "#A855F7"); // Neon Purple for ASCII Graphic
            responseWriter("System: Secure Link Initialized. Welcome to the Cybersecurity Awareness Command Center.\n", "#94A3B8");
            responseWriter("Bot: Before parsing advanced threat vectors, what is your name operator?\n\n", "#4ADE80");
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string input = txtUserInput.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            if (isFirstMessage)
            {
                ProcessMessage(input);
            }
            else
            {
                responseWriter($"{userName}: {input}\n", "#38BDF8");
                ProcessMessage(input);
            }
        }

        private void TopicButton_Click(object sender, RoutedEventArgs e)
        {
            if (isFirstMessage)
            {
                responseWriter("Bot: Please establish authentication by inputting your profile name first.\n\n", "#EF4444");
                return;
            }

            Button clickedButton = (Button)sender;
            string topicTag = clickedButton.Tag.ToString();

            responseWriter($"{userName} [Sidebar]: Tell me about {topicTag}.\n", "#38BDF8");
            ProcessMessage(topicTag);
        }

        private void ProcessMessage(string userInput)
        {
            txtUserInput.Clear();

            if (isFirstMessage)
            {
                userName = userInput;
                isFirstMessage = false;
                responseWriter($"{userName}: [Authenticated]\n", "#38BDF8");
                responseWriter($"Bot: Identity Confirmed. Welcome Operator {userName}.\n", "#4ADE80");
                responseWriter("Bot: Select a tactical awareness framework module from the left panel menu dashboard, or type natural language inquiries below.\n\n", "#4ADE80");
                rtbChatHistory.ScrollToEnd();
                return;
            }

            string botResponse = GetBotResponse(userInput);
            responseWriter($"{botResponse}\n\n", "#4ADE80");
            rtbChatHistory.ScrollToEnd();
        }

        private string GetBotResponse(string input)
        {
            string lowerInput = input.ToLower();
            string responsePrefix = "Bot: ";

            // --- 6. SENTIMENT DETECTION ---
            foreach (var emotion in sentimentEmpathy.Keys)
            {
                if (lowerInput.Contains(emotion))
                {
                    responsePrefix = sentimentEmpathy[emotion];
                    break;
                }
            }

            // --- 5. MEMORY AND RECALL (TRACK & UTILISED FAVORITE TOPICS) ---
            if (lowerInput.Contains("favorite is") || lowerInput.Contains("like to study") || lowerInput.Contains("interested in"))
            {
                foreach (var key in keywordResponses.Keys)
                {
                    if (lowerInput.Contains(key))
                    {
                        favoriteTopic = key;
                        return $"Bot: System flag updated! I will remember that you are highly interested in specializing in [{favoriteTopic.ToUpper()}] security framework infrastructure. It's a crucial part of staying safe online.";
                    }
                }
            }

            // --- 4. CONVERSATION FLOW (FOLLOW-UP CONTEXT HANDLING) ---
            if (lowerInput.Contains("more") || lowerInput.Contains("explain") || lowerInput.Contains("continue") || lowerInput.Contains("another tip"))
            {
                if (!string.IsNullOrEmpty(lastRecognizedKeyword))
                {
                    List<string> options = keywordResponses[lastRecognizedKeyword];
                    string extraTip = options[random.Next(options.Count)];
                    string baseline = $"{responsePrefix}Expanding deep-dive tactical intelligence on [{lastRecognizedKeyword.ToUpper()}] threats: {extraTip}";
                    
                    // Personalize conversational flow with recalled memory metadata if available
                    if (!string.IsNullOrEmpty(favoriteTopic) && favoriteTopic == lastRecognizedKeyword)
                    {
                        baseline += $"\nBot: Since this aligns directly with your designated favorite specialty area ({favoriteTopic}), make sure to prioritize this protocol in your weekly audits.";
                    }
                    return baseline;
                }
                return $"Bot: No prior active topic context found, {userName}. Choose a module from the sidebar dashboard to load a specific stream.";
            }

            // --- 2 & 3. KEYWORD RECOGNITION AND RANDOM RESPONSES ---
            foreach (var key in keywordResponses.Keys)
            {
                if (lowerInput.Contains(key))
                {
                    lastRecognizedKeyword = key;
                    List<string> responses = keywordResponses[key];
                    string output = $"{responsePrefix}Analyzing data for threat cluster [{key.ToUpper()}]. Attention {userName}: {responses[random.Next(responses.Count)]}";
                    
                    // Subtly inject a personalized statement if the user asks about their favorite topic later on
                    if (!string.IsNullOrEmpty(favoriteTopic) && key == favoriteTopic)
                    {
                        output += $"\nBot: [Memory Recall Note] As an operator specializing in {favoriteTopic}, you should pay close attention to this vector context.";
                    }
                    return output;
                }
            }

            // --- 7. ERROR HANDLING AND EDGE CASES (DEFAULT REPHRASE RESPONSE) ---
            return $"Bot: I am not sure I completely understand that query vector, {userName}. Could you please try rephrasing your sentence or select a known threat category from your control panel?";
        }

        // --- CORE UI RENDERER ---
        private void AppendColorText(string text, string hexColor)
        {
            TextRange tr = new TextRange(flowDoc.ContentEnd, flowDoc.ContentEnd);
            tr.Text = text;
            try
            {
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, new BrushConverter().ConvertFromString(hexColor));
            }
            catch
            {
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.White);
            }
        }
    }
}