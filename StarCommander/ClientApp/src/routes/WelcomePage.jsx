import React from "react";
import { Box, Heading, Tab, Tabs } from "grommet";
import { Message, SignInForm, SignUpForm } from "components/signin";

export default function WelcomePage() {
	return (
		<Box align="center" pad="large">
			<Heading>Star Commander</Heading>
			<Tabs>
				<Tab title="Welcome">
					<Box pad="medium">
						<SignInForm />
					</Box>
				</Tab>
				<Tab title="Need an account?">
					<Box pad="medium">
						<SignUpForm />
					</Box>
				</Tab>
			</Tabs>
			<Message />
		</Box>
	);
}
