import BirthdayList from '../components/birthday/BirthdayList';
import React, { useState, useEffect } from 'react';
import peopleApi from '../api/peopleApi';
import BirthdaysRequestForm from '../components/birthday/BirthdaysRequestForm'
import Header from '../components/common/Header';
import handleRequest from '../helpers/handleRequest';
import SlidingSection from '../components/common/SlidingSection'

function Home() {
    const initParams = {
        maxDaysLeft: 90,
        orderBy: "daysLeft",
        orderByDescending: false
    };

    const [birthdays, setBirthdays] = useState([]);
    const [requestParams, setRequestParams] = useState(initParams);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    const fetchBirthdays = async () => {
        await handleRequest(async () => peopleApi.getBirthdays(requestParams), setBirthdays, setError);
    }

    useEffect(() => {
        fetchBirthdays();
    }, []);

    return <>
        <Header text="Ближайшие дни рождения" />
        <div className="section">
            <SlidingSection title="Настроить">
                <BirthdaysRequestForm params={requestParams} setParams={setRequestParams} onSubmit={fetchBirthdays} />
            </SlidingSection>
            {
                error 
                ? (<p>{error}</p>)
                : <>
                    {isLoading && <p></p>}
                    <BirthdayList birthdays={birthdays} />
                </>
            }
        </div>
    </>
}

export default Home;