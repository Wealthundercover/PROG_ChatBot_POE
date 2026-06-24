using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbotGUI
{
    public class ChatbotEngine
    {
        private Random random = new Random();

        // STATE MANAGEMENT VARIABLES
        public string UserName { get; set; } = "";
        public string FavoriteTopic { get; set; } = "";
        public string LastRecognizedKeyword { get; set; } = "";
        public bool IsFirstMessage { get; set; } = true;

        // PART 3 EXTRAPOLATION ARCHITECTURES
        public bool IsQuizActive { get; private set; } = false;
        private int _currentQuizIndex = 0;
        private int _userQuizScore = 0;
        private List<QuizQuestion> _quizDatabase;
        private List<string> _systemActivityAuditTrail;

        // NLP TASK EXTRACTION CARRIERS
        public bool NlpExpectingTaskConfirmation { get; set; } = false;
        public string NlpPendingTaskTitle { get; private set; } = string.Empty;
        public string NlpPendingTaskDesc { get; private set; } = string.Empty;

        // CYBERSECURITY AWARENESS THREAT TOPICS DICTIONARY
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

        // SENTIMENT EMPATHICAL MATRIX DICTIONARY
        private Dictionary<string, string> sentimentEmpathy = new Dictionary<string, string>()
        {
            { "worried", "Bot: Security hazards are stressful, but implementing structural defensive controls removes the risk. " },
            { "curious", "Bot: Expanding your tactical technical awareness is the single strongest armor you can build. " },
            { "frustrated", "Bot: Defenses can feel rigorous, but operational resilience requires persistent vigilance. " }
        };

        public ChatbotEngine()
        {
            _systemActivityAuditTrail = new List<string>();

            // Setup 10 Domain-Specific Quiz Objects for evaluation
            _quizDatabase = new List<QuizQuestion>
            {
                new QuizQuestion("What protocol element does the 'S' in HTTPS signify?\nA) Speed\nB) Secure\nC) System\nD) Standard", "B", "The secure designation guarantees traffic encryption via TLS/SSL layers."),
                new QuizQuestion("Using the same password across multiple servers increases vulnerability. (True or False)", "TRUE", "Password reuse exposes systems to credential-stuffing exploits."),
                new QuizQuestion("What choice is best if you receive an unexpected email demanding urgent verification codes?\nA) Reply immediately\nB) Click to verify\nC) Delete and report via secure flags", "C", "Reporting suspect vectors immediately updates tracking filters."),
                new QuizQuestion("Antivirus signature lists provide total protection against brand-new zero-day exploits. (True or False)", "FALSE", "Zero-day vectors evade scanners until signatures are formally updated."),
                new QuizQuestion("Which architecture functions as a security filter between a trusted home network and the public internet?\nA) Network Switch\nB) Hardware Hub\nC) Firewall boundary", "C", "Firewalls screen packets based on system rules."),
                new QuizQuestion("Phishing attempts can target organizations over SMS platforms or phone lines. (True or False)", "TRUE", "Attackers use SMS (smishing) and telephone calls (vishing) alongside email."),
                new QuizQuestion("What target demographic defines a specialized 'Whaling' campaign?\nA) New Hires\nB) System Admins\nC) C-Suite Executives & Directors", "C", "Whaling vectors mimic or target top leaders to extract transactions."),
                new QuizQuestion("Open, unencrypted public Wi-Fi access configurations can expose web traffic to packet-sniffing exploits. (True or False)", "TRUE", "Unsecured Wi-Fi allows near proximity tools to capture streaming traffic data."),
                new QuizQuestion("What strategy provides superior structural password defense against brute-force attacks?\nA) Short complex word\nB) 14+ character passphrases combining unique phrases", "B", "Length scales password complexity exponentially, rendering brute force math impractical."),
                new QuizQuestion("Multi-Factor Authentication (MFA) remains vital because it stops logins even if a password is stolen. (True or False)", "TRUE", "MFA enforces a secondary, completely separate validation layer."),
                new QuizQuestion("What type of attack involves an adversary sniffing network traffic on public networks to intercept unencrypted data streams?\nA) Social Engineering\nB) Adversary-in-the-Middle (AitM)\nC) Ransomware", "B", "Adversary-in-the-Middle tactics intercept streaming wireless traffic packets directly."),
                new QuizQuestion("A clean-desk policy means locking away sensitive physical documentation when moving away from your workstation. (True or False)", "TRUE", "Physical data handling protocols require securing active assets to mitigate internal tailgating risks.")
            };

            LogSystemActivityEntry("Engine Architecture Synced & Online.");
        }

        public void LogSystemActivityEntry(string descriptiveLogString)
        {
            string structuredTimestampLog = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] - {descriptiveLogString}";
            _systemActivityAuditTrail.Add(structuredTimestampLog);
        }

        public List<string> GetLastAuditLogs()
        {
            return _systemActivityAuditTrail.Skip(Math.Max(0, _systemActivityAuditTrail.Count - 6)).ToList();
        }

        public void ForceStartQuiz()
        {
            IsQuizActive = true;
            _currentQuizIndex = 0;
            _userQuizScore = 0;
            LogSystemActivityEntry("Interactive Assessment Initialized.");
        }

        public string GetFirstQuizQuestion() => _quizDatabase[0].QuestionText;

        // CORE EVALUATION LOGIC PIPELINE
        public string CoreResponseEngine(string input)
        {
            string lowerInput = input.ToLower().Trim();

            // Intercept traffic if assessing quiz logic state
            if (IsQuizActive) return RunGameEvaluationSequence(input);
            if (NlpExpectingTaskConfirmation) return "CONFIRMATION_FLOW_ACTIVE";

            // Trigger Activity Audit Trail Retrieval Commands
            if (lowerInput.Contains("activity log") || lowerInput.Contains("have you done for me") || lowerInput.Contains("show log"))
            {
                LogSystemActivityEntry("Operational log snapshot extracted via text query.");
                return "Bot: 📋 LOG HISTORY AUDIT Snapshot:\n" + string.Join("\n", GetLastAuditLogs());
            }

            // Trigger Interactive Quiz Engine Mode
            if (lowerInput.Contains("start quiz") || lowerInput.Contains("quiz") || lowerInput.Contains("game"))
            {
                ForceStartQuiz();
                return "CONTAINS_START_QUIZ_INTENT";
            }

            // NLP Natural Language Parsing Extraction for Task Management Staging
            if (lowerInput.Contains("add task") || lowerInput.Contains("create task") || lowerInput.Contains("remind me to"))
            {
                NlpPendingTaskTitle = "Standard Mitigation Process";
                NlpPendingTaskDesc = "Generated via automated NLP command parameters input.";

                if (lowerInput.Contains("2fa") || lowerInput.Contains("two-factor") || lowerInput.Contains("mfa"))
                {
                    NlpPendingTaskTitle = "Configure Multi-Factor Passkey Sync";
                    NlpPendingTaskDesc = "Establish secondary verification checks across endpoints.";
                }
                else if (lowerInput.Contains("backup") || lowerInput.Contains("files"))
                {
                    NlpPendingTaskTitle = "Execute Secure Backup Archive";
                    NlpPendingTaskDesc = "Create an encrypted cold-storage backup array copy.";
                }

                NlpExpectingTaskConfirmation = true;
                FavoriteTopic = "Strategic Risk Tasks Matrix";
                LogSystemActivityEntry($"NLP Task Template Staged: '{NlpPendingTaskTitle}'");
                return "CONTAINS_TASK_INTENT";
            }

            string responsePrefix = "Bot: ";

            // Sentiment Engine Parser
            foreach (var emotion in sentimentEmpathy.Keys)
            {
                if (lowerInput.Contains(emotion))
                {
                    responsePrefix = sentimentEmpathy[emotion];
                    break;
                }
            }

            // Memory and Tracker Allocation Framework
            if (lowerInput.Contains("favorite is") || lowerInput.Contains("like to study") || lowerInput.Contains("interested in"))
            {
                foreach (var key in keywordResponses.Keys)
                {
                    if (lowerInput.Contains(key))
                    {
                        FavoriteTopic = key;
                        LogSystemActivityEntry($"Favorite topic flagged: '{FavoriteTopic.ToUpper()}'");
                        return $"Bot: System flag updated! I will remember that you are highly interested in specializing in [{FavoriteTopic.ToUpper()}] security framework infrastructure. It's a crucial part of staying safe online.";
                    }
                }
            }

            // Sequential Context Continuation Triggers ("More", "Continue", etc.)
            if (lowerInput.Contains("more") || lowerInput.Contains("explain") || lowerInput.Contains("continue") || lowerInput.Contains("another tip"))
            {
                if (!string.IsNullOrEmpty(LastRecognizedKeyword))
                {
                    List<string> options = keywordResponses[LastRecognizedKeyword];
                    string extraTip = options[random.Next(options.Count)];
                    string baseline = $"{responsePrefix}Expanding deep-dive tactical intelligence on [{LastRecognizedKeyword.ToUpper()}] threats: {extraTip}";

                    if (!string.IsNullOrEmpty(FavoriteTopic) && FavoriteTopic == LastRecognizedKeyword)
                    {
                        baseline += $"\nBot: Since this aligns directly with your designated favorite specialty area ({FavoriteTopic}), make sure to prioritize this protocol in your weekly audits.";
                    }
                    return baseline;
                }
                return $"Bot: No prior active topic context found, {UserName}. Choose a module from the sidebar dashboard to load a specific stream.";
            }

            // Multi-functional Keyword Match Matrix
            foreach (var key in keywordResponses.Keys)
            {
                if (lowerInput.Contains(key))
                {
                    LastRecognizedKeyword = key;
                    List<string> responses = keywordResponses[key];
                    string output = $"{responsePrefix}Analyzing data for threat cluster [{key.ToUpper()}]. Attention {UserName}: {responses[random.Next(responses.Count)]}";

                    if (!string.IsNullOrEmpty(FavoriteTopic) && key == FavoriteTopic)
                    {
                        output += $"\nBot: [Memory Recall Note] As an operator specializing in {FavoriteTopic}, you should pay close attention to this vector context.";
                    }
                    return output;
                }
            }

            // Fallback Default Rephrase Notice
            return $"Bot: I am not sure I completely understand that query vector, {UserName}. Could you please try rephrasing your sentence or select a known threat category from your control panel? (Alternatively, type 'start quiz' or 'show log').";
        }

        private string RunGameEvaluationSequence(string incomingAnswerText)
        {
            var operationalQuestion = _quizDatabase[_currentQuizIndex];
            bool evaluatesCorrect = string.Equals(incomingAnswerText.Trim(), operationalQuestion.CorrectAnswerSymbol, StringComparison.OrdinalIgnoreCase);

            if (evaluatesCorrect) _userQuizScore++;

            string evaluationFeedbackMessage = evaluatesCorrect
                ? $"Bot: ✅ ADVANTAGE MATCHED! {operationalQuestion.ExplanatoryContextNote}"
                : $"Bot: ❌ DEFENSE COMPROMISED. Resolution value is [{operationalQuestion.CorrectAnswerSymbol}]. {operationalQuestion.ExplanatoryContextNote}";

            _currentQuizIndex++;

            if (_currentQuizIndex < _quizDatabase.Count)
            {
                return $"{evaluationFeedbackMessage}\n\n========================================\nNEXT FIELD QUESTION ({_currentQuizIndex + 1}/{_quizDatabase.Count}):\n" + _quizDatabase[_currentQuizIndex].QuestionText;
            }

            IsQuizActive = false;
            LogSystemActivityEntry($"Quiz evaluated. Final score compiled: [{_userQuizScore}/{_quizDatabase.Count}]");

            string finalReviewConclusion = _userQuizScore >= 7
                ? "Bot: 🎯 PERFECT OPERATIONAL RESULT: Knowledge baseline parameters verified. Access remains granted."
                : "Bot: ⚠️ SYSTEM DEFICIT WARNING: Insufficient security baseline score. Remediate materials.";

            return $"{evaluationFeedbackMessage}\n\n🏆 CONSOLE EVALUATION MATRIX TERM COMPLETE\nFinal Compiled Score: [{_userQuizScore} / {_quizDatabase.Count}]\n\n{finalReviewConclusion}";
        }
    }

    public class QuizQuestion
    {
        public string QuestionText { get; }
        public string CorrectAnswerSymbol { get; }
        public string ExplanatoryContextNote { get; }
        public QuizQuestion(string text, string correctSymbol, string explanation)
        {
            QuestionText = text;
            CorrectAnswerSymbol = correctSymbol;
            ExplanatoryContextNote = explanation;
        }
    }
}