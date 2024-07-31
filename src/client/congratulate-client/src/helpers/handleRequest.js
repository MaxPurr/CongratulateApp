const handleRequest = async (request, onSucces, onError) => {
    try {
        const response = await request();
        if (response.status != 200) {
            onError(`Error: ${response.status} ${response.statusText}`);
            return;
        }
        onSucces(response.data);
    }
    catch(error) {
        onError(error.message);
    }
};

export default handleRequest;