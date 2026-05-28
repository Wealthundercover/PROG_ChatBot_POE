# CyberSecurity Awareness BOT - POE Part 2

## Project Overview
This is a Windows Presentation Foundation (WPF) GUI application built as the comprehensive expansion of the Cybersecurity Awareness Chatbot. Transitioning from a Command-Line Interface (CLI) to a highly responsive, custom-styled dashboard, the bot operates as an interactive "Cybersecurity Awareness Assistant" tailored for South African citizens. 

The application implements advanced programmatic logic—including custom **Delegates**, deep **Memory Tracking/Recall**, **Sentiment Analysis**, and random multi-response arrays—to provide robust, natural-language guidance across 15 distinct cybersecurity threat landscapes.

---

## Key Features

* **Advanced GUI & ASCII Integration:** * Fully translated layout using a deep-slate and neon palette designed with user-friendly accessibility principles.
    * Integrates the signature Part 1 ASCII typography header directly into a fluid, monospace `RichTextBox` command stream terminal canvas.
    * Includes dual interaction streams: natural-language inputs via a text console or rapid-fire sidebar topic macros.

* **Mandatory Delegate Architecture:** * Implements a custom structural type delegate framework (`BotResponseDelegate`) to solve programmatic UI text routing problems. This decouples core response engine logic from frontend styling blocks.

* **Memory and Recall Tracking:** * Tracks real-time context metrics. The system dynamically flags, updates, and securely remembers user data (such as their profile name and **favorite cybersecurity specialty topic**) to inject personalized call-back phrases later in the session.

* **Dynamic Text-to-Speech Engine (Optional Enhancement):** * Leverages asynchronous background threading packages (`System.Speech.Synthesis`) to audibly read back system intelligence diagnostics without blocking UI interaction responsiveness.

* **Sentiment Detection & Empathy Profiling:** * Evaluates emotional text vectors (e.g., "worried", "frustrated", "curious"). The bot alters its structural prefix headers to provide encouraging, context-appropriate support when users feel overwhelmed by cyber threat metrics.

* **15 Comprehensive Security Modules:**
    * *Core Tracks:* Phishing Dynamics, Safe Browsing, Password Armor, Scam Compliance, and Digital Privacy Controls.
    * *Advanced Vectors:* Multi-Factor Authentication (MFA), Social Engineering, Public Wi-Fi Mitigation, Data Backup Architecture (3-2-1), System Hotfixing, Device Integrity, Workspace Boundaries, Ransomware Resilience, Isolated IoT Networking, and Perimeter Access Management.

* **Robust Exception Handling & Conversational Flow:**
    * Features robust `try-catch` type parsing and safety boundaries preventing crashes during special-character injections.
    * Implements stateful persistence logic using flags like `lastRecognizedKeyword`. Users can issue ambiguous sequential tracking phrases like *"explain more"*, *"continue"*, or *"give me another tip"* without losing their place in the dialogue tree.

---

## Technical Specifications
* **Language:** C#
* **Framework:** WPF (.NET 9.0+ / Desktop Runtime Development Suite)
* **Design Engine:** XAML (Extensible Application Markup Language)
* **Architecture Pattern:** Delegate-Driven Decoupled Event Handling

---

## Git Workflow & Repository Management
* **Branch Strategy:** Maintained on a dedicated development track isolated from the Part 1 stable release line.
    * **Part 1 Baseline:** Hosted on branch `main`
    * **Part 2 GUI Overhaul:** Hosted on branch `khanban Tasks`
* **Release Protocols:** Managed via a minimum of six descriptive atomic commits with official Semantic Versioning tags tracking two releases.

## Build Status
The project utilizes a Continuous Integration (CI) pipeline powered by GitHub Actions. Every push to the workspace branches triggers automated test compilation runners to guarantee codebase execution integrity.

**Current Status:** ✅ Build Passing

## Submission Deliverables
* **Presentation Link (YouTube Walkthrough):** [https://youtu.be/1xM0vhLl3-8?si=Ztyx1Jujo1ZF0UVD](https://youtu.be/1xM0vhLl3-8?si=Ztyx1Jujo1ZF0UVD)
* **Multimedia Assets Included:** Custom `.wav` greeting files, localized ASCII typography artwork arrays, and comprehensive project solution configuration packages.

---

## Reference List (Harvard Style)

* **GitHub.** 2026. *GitHub Actions Documentation.* [Online]. Available at: https://docs.github.com/en/actions [Accessed 5 April 2026].
* **Microsoft.** 2026. *.NET Documentation.* [Online]. Available at: https://learn.microsoft.com/en-us/dotnet/ [Accessed 28 March 2026].
* **Microsoft.** 2026. *System.Media Namespace.* [Online]. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.media [Accessed 5 April 2026].
* **Microsoft.** 2026. *WPF Architecture Concepts & Delegates.* [Online]. Available at: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/ [Accessed 22 May 2026].
* **Pieterse, H.** 2021. The Cyber Threat Landscape in South Africa: A 10-Year Review. *The African Journal of Information and Communication*, 28(28), pp.1-23. doi: https://doi.org/10.23962/10539/32213. [Online]. Available at: https://www.scielo.org.za/scielo.php?pid=S2077-72132021000200003&script=sci_arttext [Accessed 22 May 2026].
* **The Independent Institute of Education (IIE).** 2026. *Programming 2A: Project of Evidence (POE) Part 2 - GUI Interface and Dynamic Responses, Sentiment Detection, and Memory.* [Unpublished Module Manual].
* **Troelsen, A. and Japikse, P.** 2022. *Pro C# 10 with .NET 6: Foundational Principles and Practices.* 11th ed. New York: Apress. [Accessed 22 May 2026].

---
**Developed by:** Wealthundercover  
**Branch Track:** `khanban Tasks`  
**Submission Date:** May 2026
