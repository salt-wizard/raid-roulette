import { SetStateAction, useEffect, useState } from 'react';
import { Accordion, AccordionDetails, AccordionSummary, Box, IconButton, Stack, TextField, Tooltip, Typography } from '@mui/material';
import UndoIcon from '@mui/icons-material/Undo';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import './RaidScreen.css';

const VetoScreen = ({sendMessage, raidList}) => {
    function unvetoTarget(_e){
        // The username is the current target value
        const eventJson = JSON.parse(_e.currentTarget.value);
        const userId = eventJson.userId;
        const json = {
            "action": "unveto",
            "target": userId
        }
        sendMessage(JSON.stringify(json));
    }

    return(
        <div>
            <Accordion defaultExpanded>
                <AccordionSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel1-content"
                    id="panel1-header"
                >
                    <Typography>Current Vito List</Typography>
                </AccordionSummary>
                <AccordionDetails>
                    {raidList !== null ? raidList.map((raidTarget) => {
                        return (
                            raidTarget["isVeto"] == true // Only show the target if they're not blacklisted, not vetoed, and online.
                            ?
                            <Stack className="raid-box" key={raidTarget["userName"] + "-key"} direction="row">
                                <Box>
                                    <Tooltip title={"Undo veto on " + raidTarget["userName"]} placement="top">
                                        <IconButton 
                                            value={'{"userId": "' + raidTarget["userId"] + '","userName": "' + raidTarget["userName"] + '"}'} 
                                            className="raid-button" 
                                            color="success" 
                                            variant="outlined"
                                            onClick={unvetoTarget}> 
                                            <UndoIcon/>
                                        </IconButton>
                                    </Tooltip>
                                </Box>
                                <img 
                                    style={{
                                        borderRadius: "50%",
                                        width: 35,
                                        height: 35,
                                        background: "red",
                                        display: "block",
                                        marginRight: "10px"
                                    }} 
                                    src={raidTarget["userPfp"]} 
                                    alt="streamer"
                                />
                                <Box 
                                sx={{ width: "26ch", marginTop: "5px" }}
                                ><Typography>{raidTarget["userName"]}</Typography></Box>
                            </Stack>
                            : null
                        )
                    })
                    : null }
                </AccordionDetails>
            </Accordion>
        </div>
    );
}

export default VetoScreen