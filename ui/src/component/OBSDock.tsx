import React from 'react';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Box from '@mui/material/Box';
import RaidScreen from './RaidScreen';

// URL for Streamer.Bot Web Socket
const OBSDock = () => {
    const [tabValue, setTabValue] = React.useState('one');

    const handleChange = (_e: React.SyntheticEvent, newValue: React.SetStateAction<string>) => {
        //console.log(e);
        setTabValue(newValue);
    };

    return(
        <div>
            <Box sx={{ width: '100%' }}>
                <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                    <Tabs
                        value={tabValue}
                        onChange={handleChange}
                        textColor="primary"
                        indicatorColor="primary"
                    >
                        <Tab value="one" label="Raid"/>
                    </Tabs>
                </Box>
                { tabValue === "one" ?
                <RaidScreen />
                : null }
            </Box>
            
        </div>
    );
}

export default OBSDock;