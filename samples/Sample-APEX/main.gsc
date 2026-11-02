// APEX Zombies - UI and controller navigation prototype v0.1
// BO3 PS4 (tested compiler: APEX.Compiler, based on PS4 BO3 1.33 GSC source)
// No gameplay modification features are included.
// Open: L2 + R3; move: D-pad UP/DOWN; select: Square; back: R3.
// Settings preview: Square to edit, Left/Right to adjust, Square to save, R3 to cancel.

autoexec main()
{
    level thread apexBootstrap();
}

apexBootstrap()
{
    level endon("game_ended");

    while (1)
    {
        players = GetPlayers();
        for (i = 0; i < players.size; i++)
        {
            if (isDefined(players[i]) && players[i] IsHost())
            {
                if (!isDefined(players[i].apexStarted))
                {
                    players[i].apexStarted = 1;
                    players[i] thread apexClient();
                }
            }
        }
        wait 1;
    }
}

apexClient()
{
    self endon("disconnect");
    level endon("game_ended");

    // Autoexec can start before the player is in the match.
    while (!isDefined(self.sessionstate) || self.sessionstate != "playing")
        wait 0.25;

    wait 1;

    self.apexOpen = 0;
    self.apexPage = 0;       // 0 = root; 1..6 = placeholders; 7 = settings.
    self.apexSelected = 0;
    self.apexAdjusting = 0;
    self.apexFovPreview = 90;
    self.apexFovOriginal = 90;
    self.apexDemoToggle = 0;

    self apexCreateUI();
	self thread apexReadyNotification();

    wasCombo = 0;
    wasMelee = 0;
    wasUp = 0;
    wasDown = 0;
    wasLeft = 0;
    wasRight = 0;
    wasSelect = 0;

    while (1)
    {
        aim = self AdsButtonPressed();            // L2
        melee = self MeleeButtonPressed();         // R3
        up = self ActionSlotOneButtonPressed();    // D-pad Up
        down = self ActionSlotTwoButtonPressed();  // D-pad Down
        left = self ActionSlotThreeButtonPressed();// D-pad Left
        right = self ActionSlotFourButtonPressed();// D-pad Right
        select = self UseButtonPressed();          // Square
        combo = aim && melee;

        if (!self.apexOpen)
        {
            // Edge detect the combo, rather than opening every frame it is held.
            if (combo && !wasCombo)
            {
                self.apexOpen = 1;
                self.apexPage = 0;
                self.apexSelected = 0;
                self.apexAdjusting = 0;
                self apexShowUI();
                self apexRender();
            }
        }
        else
        {
            if (self.apexAdjusting)
            {
                if (left && !wasLeft)
                {
                    self.apexFovPreview -= 5;
                    if (self.apexFovPreview < 60)
                        self.apexFovPreview = 60;
                    self apexRender();
                }
                if (right && !wasRight)
                {
                    self.apexFovPreview += 5;
                    if (self.apexFovPreview > 120)
                        self.apexFovPreview = 120;
                    self apexRender();
                }
                if (select && !wasSelect)
                {
                    self.apexAdjusting = 0;   // Accept preview value.
                    self apexRender();
                }
                if (melee && !wasMelee && !aim)
                {
                    self.apexFovPreview = self.apexFovOriginal; // Cancel.
                    self.apexAdjusting = 0;
                    self apexRender();
                }
            }
            else
            {
                if (up && !wasUp)
                {
                    self.apexSelected--;
                    if (self.apexSelected < 0)
                        self.apexSelected = self apexCount() - 1;
                    self apexRender();
                }
                if (down && !wasDown)
                {
                    self.apexSelected++;
                    if (self.apexSelected >= self apexCount())
                        self.apexSelected = 0;
                    self apexRender();
                }
                if (select && !wasSelect)
                    self apexSelect();

                // R3 is Back, but not if L2 is held (the open combo).
                if (melee && !wasMelee && !aim)
                    self apexBack();
            }
        }

        wasCombo = combo;
        wasMelee = melee;
        wasUp = up;
        wasDown = down;
        wasLeft = left;
        wasRight = right;
        wasSelect = select;
        wait 0.05;
    }
}

apexCreateUI()
{
    // Basic BO3 client HUD elements. No LUI or external scripts required.
    self.apexPanel = self apexRectangle(23, 67, 273, 309, (0.018, 0.045, 0.080), 0);
    self.apexTopLine = self apexRectangle(23, 67, 273, 4, (0.02, 0.78, 1), 0);
    self.apexHighlight = self apexRectangle(29, 140, 259, 25, (0.01, 0.35, 0.55), 0);

    self.apexTitle = self apexText(39, 81, 2.0,(0.06, 0.82, 1),"APEX");

	self.apexSubTitle = self apexText(40, 111, 1.5,(0.58, 0.76, 0.86),"ZOMBIES / v0.5");

	self.apexFooter = self apexText(38, 347, 0.5,(0.58, 0.85, 0.95),"");

    self.apexRows = [];
    for (i = 0; i < 7; i++)
        self.apexRows[i] = self apexText(39, 143 + (i * 27), 1.18, (0.85, 0.92, 1), " ");
}

apexRectangle(x, y, width, height, tint, opacity)
{
    e = NewClientHudElem(self);
    e.horzAlign = "user_left";
    e.vertAlign = "user_top";
    e.alignX = "left";
    e.alignY = "top";
    e.x = x;
    e.y = y;
    e.sort = 5;
    e.foreground = true;
    e.hideWhenInMenu = true;
    e.hideWhenDead = false;
    e.color = tint;
    e.alpha = opacity;
    e setShader("white", width, height);
    return e;
}

apexText(x, y, scale, tint, value)
{
    e = NewClientHudElem(self);

    e.horzAlign = "user_left";
    e.vertAlign = "user_top";
    e.alignX = "left";
    e.alignY = "top";

    e.x = x;
    e.y = y;

    e.sort = 10;
    e.foreground = true;

    e.hideWhenInMenu = true;
    e.hideWhenDead = false;

    e.color = tint;
    e.alpha = 0;

    // Set the requested font size.
    e.fontScale = scale;

    // Assign the text after configuring its size.
    e setText(value);

    return e;
}

apexShowUI()
{
    self.apexPanel.alpha = 0.87;
    self.apexTopLine.alpha = 1;
    self.apexHighlight.alpha = 0.85;
    self.apexTitle.alpha = 1;
    self.apexSubTitle.alpha = 1;
    self.apexFooter.alpha = 1;
}

apexHideUI()
{
    self.apexPanel.alpha = 0;
    self.apexTopLine.alpha = 0;
    self.apexHighlight.alpha = 0;
    self.apexTitle.alpha = 0;
    self.apexSubTitle.alpha = 0;
    self.apexFooter.alpha = 0;

    for (i = 0; i < 7; i++)
        self.apexRows[i].alpha = 0;
}

apexCount()
{
    if (self.apexPage == 0)
        return 7;
    if (self.apexPage == 7)
        return 3;
    return 2;
}

apexLabel(index)
{
    if (self.apexPage == 0)
    {
        switch (index)
        {
            case 0: return "PLAYER OPTIONS     >";
            case 1: return "WEAPON OPTIONS     >";
            case 2: return "ZOMBIE OPTIONS     >";
            case 3: return "GAMEPLAY OPTIONS   >";
            case 4: return "TELEPORTATION      >";
            case 5: return "VISUAL OPTIONS     >";
            case 6: return "SETTINGS           >";
        }
    }
    if (self.apexPage == 7)
    {
        switch (index)
        {
            case 0: return "FOV PREVIEW: " + self.apexFovPreview;
            case 1:
                if (self.apexDemoToggle)
                    return "DEMO TOGGLE: ON";
                return "DEMO TOGGLE: OFF";
            case 2: return "< BACK";
        }
    }
    if (index == 0)
        return "FEATURES COMING SOON";
    if (index == 1)
        return "< BACK";
    return "";
}

apexPageName()
{
    switch (self.apexPage)
    {
        case 0: return "MAIN MENU";
        case 1: return "PLAYER OPTIONS";
        case 2: return "WEAPON OPTIONS";
        case 3: return "ZOMBIE OPTIONS";
        case 4: return "GAMEPLAY OPTIONS";
        case 5: return "TELEPORTATION";
        case 6: return "VISUAL OPTIONS";
        case 7: return "SETTINGS";
    }
    return "APEX";
}

apexRender()
{
    if (!self.apexOpen)
        return;

    self.apexSubTitle setText(self apexPageName() + "  /  v0.2");
    self.apexHighlight.y = 140 + (self.apexSelected * 27);

    for (i = 0; i < 7; i++)
    {
        if (i < self apexCount())
        {
            self.apexRows[i] setText(self apexLabel(i));
            self.apexRows[i].alpha = 1;
            if (i == self.apexSelected)
                self.apexRows[i].color = (0.14, 0.93, 1);
            else
                self.apexRows[i].color = (0.83, 0.90, 0.95);
        }
        else
            self.apexRows[i].alpha = 0;
    }

	self.apexFooter.fontScale = 1.0;
	self.apexFooter.color = (0.45, 0.65, 0.75);

    if (self.apexAdjusting)
	{
		self.apexFooter setText("LEFT/RIGHT | SQUARE SAVE | R3 CANCEL");
	}
	else
	{
		self.apexFooter setText("UP/DOWN | SQUARE SELECT | R3 BACK");
	}
}

apexSelect()
{
    if (self.apexPage == 0)
    {
        self.apexPage = self.apexSelected + 1;
        self.apexSelected = 0;
        self apexRender();
        return;
    }

    if (self.apexPage == 7)
    {
        if (self.apexSelected == 0)
        {
            self.apexFovOriginal = self.apexFovPreview;
            self.apexAdjusting = 1;
            self apexRender();
        }
        else if (self.apexSelected == 1)
        {
            self.apexDemoToggle = !self.apexDemoToggle;
            self apexRender();
        }
        else
            self apexBack();
        return;
    }

    if (self.apexSelected == 1)
        self apexBack();
    // Placeholder row deliberately does nothing.
}

apexBack()
{
    if (self.apexPage == 0)
    {
        self.apexOpen = 0;
        self.apexAdjusting = 0;
        self apexHideUI();
    }
    else
    {
        self.apexSelected = self.apexPage - 1; // Restore selected category.
        self.apexPage = 0;
        self apexRender();
    }
}

apexReadyNotification()
{
    self endon("disconnect");
    level endon("game_ended");

    // Allow time for the Zombies intro cinematic.
    wait 15;

    self IPrintLnBold("^5APEX ^7READY - L2 + R3");
}