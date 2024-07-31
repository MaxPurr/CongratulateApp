import PersonList from '../components/person/PersonList';
import React, { useState, useEffect } from 'react';
import peopleApi from '../api/peopleApi';
import Header from '../components/common/Header';
import PeopleRequestForm from '../components/person/PeopleRequestForm'
import handleRequest from '../helpers/handleRequest';
import PersonAddForm from '../components/person/PersonAddForm';
import SlidingSection from '../components/common/SlidingSection'

function List() {
    const initParams = {
        orderBy: "name",
        orderByDescending: false
    };
    
    const [people, setPeople] = useState([]);
    const [requestParams, setRequestParams] = useState(initParams);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

    const fetchPeople = async () => {
        await handleRequest(async () => peopleApi.getList(requestParams), setPeople, setError);
    }

    const onDelete = async (personId) => {
        await handleRequest(async () => peopleApi.delete(personId), async () => fetchPeople(), setError);
    }

    const addPerson = async ({person, image}) => {
        setIsLoading(true);
        try {
            const response = await peopleApi.add(person);
            console.log(response);
            if(response.status != 200) {
                setError(`Error: ${response.status} ${response.statusText}`);
                return;
            }
            const {id} = response.data;
            const result = await peopleApi.uploadPhoto(id, image);
            console.log(result);
        }
        catch(error) {
            setError(error.message);
        }
        finally {
            await fetchPeople();
            setIsLoading(false);
        }
    }

    useEffect(() => {
        fetchPeople();
    }, []);

    return <>
        <Header text="Список друзей" />
        <div className="section">
            <SlidingSection title="Добавить друга">
                <PersonAddForm onSubmit={addPerson} />
            </SlidingSection>
            <SlidingSection title="Настроить">
                <PeopleRequestForm params={requestParams} setParams={setRequestParams} onSubmit={fetchPeople} />
            </SlidingSection>
            {
                error 
                ? (<p>{error}</p>)
                : <>
                    {isLoading && <p></p>}
                    <PersonList people={people} onDelete={onDelete}/>
                </>
            }
        </div>
    </>
}

export default List;